namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class DockManager : DockContainer
    {
        private DockPanel activeDoc;
        private Collection<DockContainer> autoHideB;
        private DockContainer autoHideContainer;
        private Collection<DockContainer> autoHideL;
        private Collection<DockContainer> autoHideR;
        private Collection<DockContainer> autoHideT;
        private Container components;
        private static ArrayList contList = new ArrayList();
        private static OverlayForm dockGuide = null;
        private static DockEventHandler dragEvent;
        private static bool fastMoveDraw = false;
        private static ArrayList formList = new ArrayList();
        private static Collection<DockPanel> listDocument = new Collection<DockPanel>();
        private static Collection<DockPanel> listPanel = new Collection<DockPanel>();
        private static Collection<DockPanel> listTool = new Collection<DockPanel>();
        private static ArrayList managerList = new ArrayList();
        private static bool noGuidePlease = false;
        private static DockVisualStyle style = DockVisualStyle.VS2005;

        public static  event FormLoadEventHandler FormLoad;

        public DockManager()
        {
            this.components = null;
            this.activeDoc = null;
            this.autoHideContainer = null;
            this.autoHideL = new Collection<DockContainer>();
            this.autoHideT = new Collection<DockContainer>();
            this.autoHideR = new Collection<DockContainer>();
            this.autoHideB = new Collection<DockContainer>();
            this.InitializeComponent();
            this.Init();
        }

        public DockManager(IContainer container)
        {
            this.components = null;
            this.activeDoc = null;
            this.autoHideContainer = null;
            this.autoHideL = new Collection<DockContainer>();
            this.autoHideT = new Collection<DockContainer>();
            this.autoHideR = new Collection<DockContainer>();
            this.autoHideB = new Collection<DockContainer>();
            container.Add(this);
            this.InitializeComponent();
            this.Init();
        }

        private void ActivateParent(object sender, EventArgs e)
        {
            try
            {
                if (this.activeDoc != null)
                {
                    this.activeDoc.SetFocus(true);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockManager.ActivateParent: " + exception.Message);
            }
            finally
            {
                base.Invalidate(true);
            }
        }

        internal void AutoHideContainer(DockContainer c, DockStyle dst, bool hide)
        {
            if (c != null)
            {
                switch (dst)
                {
                    case DockStyle.Top:
                        base.dockOffsetT = this.UpdateAutoHideList(c, hide, this.autoHideT);
                        break;

                    case DockStyle.Bottom:
                        base.dockOffsetB = this.UpdateAutoHideList(c, hide, this.autoHideB);
                        break;

                    case DockStyle.Left:
                        base.dockOffsetL = this.UpdateAutoHideList(c, hide, this.autoHideL);
                        break;

                    case DockStyle.Right:
                        base.dockOffsetR = this.UpdateAutoHideList(c, hide, this.autoHideR);
                        break;

                    default:
                        base.dockOffsetL = this.UpdateAutoHideList(c, false, this.autoHideL);
                        base.dockOffsetT = this.UpdateAutoHideList(c, false, this.autoHideT);
                        base.dockOffsetR = this.UpdateAutoHideList(c, false, this.autoHideR);
                        base.dockOffsetB = this.UpdateAutoHideList(c, false, this.autoHideB);
                        break;
                }
                base.AdjustBorders();
            }
        }

        public static void CloseAll()
        {
            CloseTools();
            CloseDocuments();
        }

        public static void CloseDocuments()
        {
            Collection<DockWindow> collection = new Collection<DockWindow>();
            foreach (DockPanel panel in listDocument)
            {
                collection.Add(panel.Form);
            }
            foreach (DockWindow window in collection)
            {
                window.Close();
            }
            collection.Clear();
        }

        public static void CloseTools()
        {
            Collection<DockWindow> collection = new Collection<DockWindow>();
            foreach (DockPanel panel in listTool)
            {
                collection.Add(panel.Form);
            }
            foreach (DockWindow window in collection)
            {
                window.Close();
            }
            collection.Clear();
        }

        private void DeactivateParent(object sender, EventArgs e)
        {
            try
            {
                foreach (DockPanel panel in ListDocument)
                {
                    DockContainer hostContainer = panel.Form.HostContainer;
                    if ((hostContainer != null) && ((hostContainer.ActivePanel == panel) && hostContainer.ContainsFocus))
                    {
                        this.activeDoc = panel;
                        panel.SetFocus(false);
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockManager.DeactivateParent: " + exception.Message);
            }
            finally
            {
                base.Invalidate(true);
            }
        }

        public static void DebugFormList()
        {
            foreach (DockForm form in formList)
            {
                Console.WriteLine(form.Text);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        internal static void FormActivated(DockForm form)
        {
            if (formList.Contains(form))
            {
                formList.Remove(form);
                formList.Insert(0, form);
            }
        }

        internal static DockForm GetFormAtPoint(Point pt, int startIndex)
        {
            for (int i = startIndex; i < formList.Count; i++)
            {
                DockForm form = formList[i] as DockForm;
                if (form.Bounds.Contains(pt) & form.Visible)
                {
                    return form;
                }
            }
            return null;
        }

        internal static int GetZIndex(DockForm form)
        {
            return formList.IndexOf(form);
        }

        internal static void HideDockGuide()
        {
            if (dockGuide != null)
            {
                dockGuide.Hide();
            }
        }

        private void Init()
        {
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DockType = DockContainerType.Document;
            base.removeable = false;
            base.dragWindowHandler = new DockEventHandler(this.DragWindow);
            RegisterManager(this);
        }

        private void InitializeComponent()
        {
        }

        internal static void InvokeDragEvent(object sender, DockEventArgs e)
        {
            if (dragEvent != null)
            {
                dragEvent(sender, e);
            }
            if (!e.ShowDockGuide)
            {
                HideDockGuide();
            }
        }

        internal static void InvokeFormLoad(object sender, FormLoadEventArgs e)
        {
            if (FormLoad != null)
            {
                FormLoad(sender, e);
            }
        }

        private static void ObjectDisposed(object sender, EventArgs e)
        {
            if (sender is DockContainer)
            {
                UnRegisterContainer(sender as DockContainer);
            }
            else if (sender is DockForm)
            {
                UnRegisterForm(sender as DockForm);
            }
            else if (sender is DockManager)
            {
                UnRegisterManager(sender as DockManager);
            }
            else
            {
                if (!(sender is DockWindow))
                {
                    throw new ArgumentException("Only DockForm, DockContainer, DockManager and DockWindow objects may be handled by the DockServer.");
                }
                UnRegisterWindow(sender as DockWindow);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if ((!(e.Control is DockContainer) && !(e.Control is DockPanel)) && !(e.Control is FlatButton))
            {
                if (base.Parent != null)
                {
                    base.Parent.Controls.Add(e.Control);
                }
                else
                {
                    base.Controls.Remove(e.Control);
                }
                base.Invalidate();
            }
            else
            {
                base.OnControlAdded(e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (((this.autoHideContainer != null) && !this.autoHideContainer.fading) && !this.autoHideContainer.RectangleToScreen(this.autoHideContainer.ClientRectangle).Contains(Control.MousePosition.X, Control.MousePosition.Y))
            {
                this.autoHideContainer.FadeOut();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((this.autoHideContainer == null) || !this.autoHideContainer.fading)
            {
                Rectangle rectangle;
                Size size;
                foreach (DockContainer container in this.autoHideL)
                {
                    foreach (DockPanel panel in container.panList)
                    {
                        rectangle = new Rectangle((int) panel.TabRect.Left, (int) panel.TabRect.Top, (int) panel.TabRect.Width, (int) panel.TabRect.Height);
                        if (base.RectangleToScreen(rectangle).Contains(Control.MousePosition.X, Control.MousePosition.Y))
                        {
                            container.ActivePanel = panel;
                            if (container != this.autoHideContainer)
                            {
                                base.disableOnControlAdded = true;
                                base.disableOnControlRemove = true;
                                base.Controls.Remove(this.autoHideContainer);
                                this.autoHideContainer = container;
                                size = container.Size;
                                container.Dock = DockStyle.None;
                                container.Location = new Point(base.dockOffsetL, base.dockOffsetT);
                                container.Size = new Size(size.Width, (base.Height - base.dockOffsetT) - base.dockOffsetB);
                                container.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                                base.Controls.Add(container);
                                container.FadeIn();
                                base.disableOnControlAdded = false;
                                base.disableOnControlRemove = false;
                            }
                            return;
                        }
                    }
                }
                foreach (DockContainer container in this.autoHideR)
                {
                    foreach (DockPanel panel in container.panList)
                    {
                        rectangle = new Rectangle((int) panel.TabRect.Left, (int) panel.TabRect.Top, (int) panel.TabRect.Width, (int) panel.TabRect.Height);
                        if (base.RectangleToScreen(rectangle).Contains(Control.MousePosition.X, Control.MousePosition.Y))
                        {
                            container.ActivePanel = panel;
                            if (container != this.autoHideContainer)
                            {
                                base.Controls.Remove(this.autoHideContainer);
                                this.autoHideContainer = container;
                                size = container.Size;
                                container.Dock = DockStyle.None;
                                container.Location = new Point((base.Width - size.Width) - base.dockOffsetR, base.dockOffsetT);
                                container.Size = new Size(size.Width, (base.Height - base.dockOffsetT) - base.dockOffsetB);
                                container.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                                base.Controls.Add(container);
                                container.FadeIn();
                            }
                            return;
                        }
                    }
                }
                foreach (DockContainer container in this.autoHideT)
                {
                    foreach (DockPanel panel in container.panList)
                    {
                        rectangle = new Rectangle((int) panel.TabRect.Left, (int) panel.TabRect.Top, (int) panel.TabRect.Width, (int) panel.TabRect.Height);
                        if (base.RectangleToScreen(rectangle).Contains(Control.MousePosition.X, Control.MousePosition.Y))
                        {
                            container.ActivePanel = panel;
                            if (container != this.autoHideContainer)
                            {
                                base.Controls.Remove(this.autoHideContainer);
                                this.autoHideContainer = container;
                                size = container.Size;
                                container.Dock = DockStyle.None;
                                container.Location = new Point(base.dockOffsetL, base.dockOffsetT);
                                container.Size = new Size((base.Width - base.dockOffsetL) - base.dockOffsetR, size.Height);
                                container.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                                base.Controls.Add(container);
                                container.FadeIn();
                            }
                            return;
                        }
                    }
                }
                foreach (DockContainer container in this.autoHideB)
                {
                    foreach (DockPanel panel in container.panList)
                    {
                        rectangle = new Rectangle((int) panel.TabRect.Left, (int) panel.TabRect.Top, (int) panel.TabRect.Width, (int) panel.TabRect.Height);
                        if (base.RectangleToScreen(rectangle).Contains(Control.MousePosition.X, Control.MousePosition.Y))
                        {
                            container.ActivePanel = panel;
                            if (container != this.autoHideContainer)
                            {
                                base.Controls.Remove(this.autoHideContainer);
                                this.autoHideContainer = container;
                                size = container.Size;
                                container.Dock = DockStyle.None;
                                container.Location = new Point(base.dockOffsetL, (base.Height - size.Height) - base.dockOffsetB);
                                container.Size = new Size((base.Width - base.dockOffsetL) - base.dockOffsetR, size.Height);
                                container.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                                base.Controls.Add(container);
                                container.FadeIn();
                            }
                            return;
                        }
                    }
                }
                if ((this.autoHideContainer != null) && !this.autoHideContainer.RectangleToScreen(this.autoHideContainer.ClientRectangle).Contains(Control.MousePosition.X, Control.MousePosition.Y))
                {
                    this.autoHideContainer.FadeOut();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SizeF ef;
            StringFormat genericDefault = StringFormat.GenericDefault;
            Graphics graphics = e.Graphics;
            int num = 3;
            int num2 = 0;
            int height = 0;
            genericDefault.Trimming = StringTrimming.EllipsisCharacter;
            if (base.panList.Count == 0)
            {
                graphics.Clear(SystemColors.Control);
                Rectangle rect = new Rectangle(base.dockOffsetL, base.dockOffsetT, (base.Width - base.dockOffsetL) - base.dockOffsetR, (base.Height - base.dockOffsetT) - base.dockOffsetB);
                graphics.FillRectangle(SystemBrushes.ControlDark, rect);
            }
            else
            {
                base.OnPaint(e);
            }
            num = 3 + base.dockOffsetL;
            foreach (DockContainer container in this.autoHideT)
            {
                foreach (DockPanel panel in container.panList)
                {
                    ef = graphics.MeasureString(panel.Form.Text, this.Font);
                    num2 = (num + ((int) ef.Width)) + 0x12;
                    graphics.DrawString(panel.Form.Text, this.Font, SystemBrushes.ControlDarkDark, (float) (num + 0x12), 3f);
                    graphics.DrawIcon(panel.Form.Icon, new Rectangle(num + 3, 2, 0x10, 0x10));
                    graphics.DrawLine(SystemPens.ControlDark, num, 0, num, 0x11);
                    graphics.DrawLine(SystemPens.ControlDark, num, 0x11, num + 2, 0x13);
                    graphics.DrawLine(SystemPens.ControlDark, num + 2, 0x13, num2 - 2, 0x13);
                    graphics.DrawLine(SystemPens.ControlDark, num2 - 2, 0x13, num2, 0x11);
                    graphics.DrawLine(SystemPens.ControlDark, num2, 0, num2, 0x11);
                    ef.Height = 20f;
                    ef.Width += 18f;
                    panel.TabRect = new RectangleF((float) num, 0f, ef.Width, (float) base.dockOffsetT);
                    num += (int) ef.Width;
                }
                num += 0x10;
            }
            num = 3 + base.dockOffsetL;
            height = base.Height;
            foreach (DockContainer container in this.autoHideB)
            {
                foreach (DockPanel panel in container.panList)
                {
                    ef = graphics.MeasureString(panel.Form.Text, this.Font);
                    num2 = (num + ((int) ef.Width)) + 0x12;
                    graphics.DrawString(panel.Form.Text, this.Font, SystemBrushes.ControlDarkDark, (float) (num + 0x12), (height - 3) - ef.Height);
                    graphics.DrawIcon(panel.Form.Icon, new Rectangle(num + 3, (base.Height - base.dockOffsetB) + 5, 0x10, 0x10));
                    graphics.DrawLine(SystemPens.ControlDark, num, height, num, height - 0x11);
                    graphics.DrawLine(SystemPens.ControlDark, num, height - 0x11, num + 2, height - 0x13);
                    graphics.DrawLine(SystemPens.ControlDark, (int) (num + 2), (int) (height - 0x13), (int) (num2 - 2), (int) (height - 0x13));
                    graphics.DrawLine(SystemPens.ControlDark, num2 - 2, height - 0x13, num2, height - 0x11);
                    graphics.DrawLine(SystemPens.ControlDark, num2, height, num2, height - 0x11);
                    ef.Height = 20f;
                    ef.Width += 18f;
                    panel.TabRect = new RectangleF((float) num, (base.Height - ef.Height) - 2f, ef.Width, (float) (base.dockOffsetB + 2));
                    num += (int) ef.Width;
                }
                num += 0x10;
            }
            graphics.RotateTransform(90f);
            num = 3 + base.dockOffsetT;
            foreach (DockContainer container in this.autoHideL)
            {
                foreach (DockPanel panel in container.panList)
                {
                    ef = graphics.MeasureString(panel.Form.Text, this.Font);
                    num2 = (num + ((int) ef.Width)) + 0x12;
                    graphics.DrawString(panel.Form.Text, this.Font, SystemBrushes.ControlDarkDark, (float) (num + 0x12), -ef.Height);
                    graphics.DrawIcon(panel.Form.Icon, new Rectangle(2, num + 3, 0x10, 0x10));
                    graphics.DrawLine(SystemPens.ControlDark, num, 0, num, -17);
                    graphics.DrawLine(SystemPens.ControlDark, num, -17, num + 2, -19);
                    graphics.DrawLine(SystemPens.ControlDark, num + 2, -19, num2 - 2, -19);
                    graphics.DrawLine(SystemPens.ControlDark, num2 - 2, -19, num2, -17);
                    graphics.DrawLine(SystemPens.ControlDark, num2, 0, num2, -17);
                    ef.Height = 20f;
                    ef.Width += 18f;
                    panel.TabRect = new RectangleF(0f, (float) num, (float) base.dockOffsetL, ef.Width);
                    num += (int) ef.Width;
                }
                num += 0x10;
            }
            num = 3 + base.dockOffsetT;
            height = -base.Width;
            foreach (DockContainer container in this.autoHideR)
            {
                foreach (DockPanel panel in container.panList)
                {
                    ef = graphics.MeasureString(panel.Form.Text, this.Font);
                    num2 = (num + ((int) ef.Width)) + 0x12;
                    graphics.DrawString(panel.Form.Text, this.Font, SystemBrushes.ControlDarkDark, (float) (num + 0x12), (float) (height + 3));
                    graphics.DrawIcon(panel.Form.Icon, new Rectangle((base.Width - base.dockOffsetR) + 5, num + 3, 0x10, 0x10));
                    graphics.DrawLine(SystemPens.ControlDark, num, height, num, height + 0x11);
                    graphics.DrawLine(SystemPens.ControlDark, num, height + 0x11, num + 2, height + 0x13);
                    graphics.DrawLine(SystemPens.ControlDark, (int) (num + 2), (int) (height + 0x13), (int) (num2 - 2), (int) (height + 0x13));
                    graphics.DrawLine(SystemPens.ControlDark, num2 - 2, height + 0x13, num2, height + 0x11);
                    graphics.DrawLine(SystemPens.ControlDark, num2, height, num2, height + 0x11);
                    ef.Height = 20f;
                    ef.Width += 18f;
                    panel.TabRect = new RectangleF((base.Width - ef.Height) - 2f, (float) num, (float) base.dockOffsetR, ef.Width + 2f);
                    num += (int) ef.Width;
                }
                num += 0x10;
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (base.Parent is Form)
            {
                Form parent = base.Parent as Form;
                parent.KeyDown += new KeyEventHandler(this.InvokeKeyDown);
                parent.KeyUp += new KeyEventHandler(this.InvokeKeyUp);
                parent.Deactivate += new EventHandler(this.DeactivateParent);
                parent.Activated += new EventHandler(this.ActivateParent);
            }
            base.OnParentChanged(e);
        }

        public static void ReadXml(string file)
        {
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(file) {
                    WhitespaceHandling = WhitespaceHandling.None
                };
                while (reader.Read())
                {
                    string str;
                    if (!reader.IsStartElement())
                    {
                        continue;
                    }
                    string name = reader.Name;
                    if (name != null)
                    {
                        if (!(name == "form"))
                        {
                            if (name == "manager")
                            {
                                goto Label_0099;
                            }
                        }
                        else
                        {
                            DockForm form = new DockForm {
                                Opacity = 0.0
                            };
                            form.Show();
                            form.ReadXml(reader.ReadSubtree());
                            form.Opacity = 1.0;
                        }
                    }
                    continue;
                Label_0099:
                    str = reader.GetAttribute("parent");
                    if (str != null)
                    {
                        foreach (DockManager manager in managerList)
                        {
                            if (manager.Parent.GetType().FullName == str)
                            {
                                manager.ReadXml(reader.ReadSubtree(), true);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        internal override void ReadXml(XmlReader reader, bool dockManagerHead)
        {
            base.ReadXml(reader, dockManagerHead);
        }

        internal static void RegisterContainer(DockContainer cont)
        {
            if (!contList.Contains(cont))
            {
                if (cont == null)
                {
                    throw new ArgumentNullException("The container must not be null.");
                }
                cont.Disposed += new EventHandler(DockManager.ObjectDisposed);
                contList.Add(cont);
                dragEvent = (DockEventHandler) Delegate.Combine(dragEvent, cont.dragWindowHandler);
            }
        }

        internal static void RegisterForm(DockForm form)
        {
            if (!formList.Contains(form))
            {
                if (form == null)
                {
                    throw new ArgumentNullException("The form must not be null.");
                }
                form.Disposed += new EventHandler(DockManager.ObjectDisposed);
                formList.Add(form);
            }
        }

        internal static void RegisterManager(DockManager manager)
        {
            if (!managerList.Contains(manager))
            {
                if (manager == null)
                {
                    throw new ArgumentNullException("The manager must not be null.");
                }
                manager.Disposed += new EventHandler(DockManager.ObjectDisposed);
                managerList.Add(manager);
            }
        }

        internal static void RegisterWindow(DockWindow wnd)
        {
            if (wnd == null)
            {
                throw new ArgumentNullException("The window must not be null.");
            }
            if (!listPanel.Contains(wnd.ControlContainer))
            {
                wnd.Disposed += new EventHandler(DockManager.ObjectDisposed);
                listPanel.Add(wnd.ControlContainer);
                if (wnd.DockType == DockContainerType.Document)
                {
                    listDocument.Add(wnd.ControlContainer);
                }
                else if (wnd.DockType == DockContainerType.ToolWindow)
                {
                    listTool.Add(wnd.ControlContainer);
                }
            }
        }

        internal void ReleaseAutoHideContainer()
        {
            base.Controls.Remove(this.autoHideContainer);
            this.autoHideContainer = null;
        }

        internal static void UnRegisterContainer(DockContainer cont)
        {
            if (contList.Contains(cont))
            {
                dragEvent = (DockEventHandler) Delegate.Remove(dragEvent, cont.dragWindowHandler);
                contList.Remove(cont);
            }
        }

        internal static void UnRegisterForm(DockForm form)
        {
            if (formList.Contains(form))
            {
                formList.Remove(form);
            }
        }

        internal static void UnRegisterManager(DockManager manager)
        {
            if (managerList.Contains(manager))
            {
                managerList.Remove(manager);
            }
        }

        internal static void UnRegisterWindow(DockWindow wnd)
        {
            if (wnd == null)
            {
                throw new ArgumentNullException("The window must not be null.");
            }
            if (listPanel.Contains(wnd.ControlContainer))
            {
                contList.Remove(wnd.ControlContainer);
                if (wnd.DockType == DockContainerType.Document)
                {
                    listDocument.Remove(wnd.ControlContainer);
                }
                else if (wnd.DockType == DockContainerType.ToolWindow)
                {
                    listTool.Remove(wnd.ControlContainer);
                }
            }
        }

        private int UpdateAutoHideList(DockContainer c, bool hide, Collection<DockContainer> list)
        {
            if (hide)
            {
                list.Add(c);
            }
            else if (list.Contains(c))
            {
                this.autoHideContainer = null;
                list.Remove(c);
                base.Controls.Remove(c);
            }
            if (list.Count > 0)
            {
                return 0x16;
            }
            return 0;
        }

        internal static void UpdateDockGuide(DockContainer target, DockEventArgs e)
        {
            if ((((target == null) | noGuidePlease) | (DockManager.style != DockVisualStyle.VS2005)) | fastMoveDraw)
            {
                HideDockGuide();
            }
            else
            {
                if (dockGuide == null)
                {
                    dockGuide = new OverlayForm();
                }
                Size size = dockGuide.Size;
                Point location = dockGuide.Location;
                dockGuide.TargetHost = target;
                dockGuide.Size = target.Size;
                if (!dockGuide.Visible)
                {
                    dockGuide.Show();
                }
                if (target.Parent != null)
                {
                    dockGuide.Location = target.RectangleToScreen(target.ClientRectangle).Location;
                }
                else
                {
                    dockGuide.Location = target.Location;
                }
                if ((dockGuide.Location != location) || (dockGuide.Size != size))
                {
                    dockGuide.Invalidate();
                }
                dockGuide.BringToFront();
                DockStyle style = dockGuide.HitTest(e.Point);
                if (style != DockStyle.None)
                {
                    e.Point = target.GetVirtualDragDest(style);
                }
                else
                {
                    e.Handled = true;
                }
                e.ShowDockGuide = true;
            }
        }

        public static void WriteXml(string file)
        {
            XmlTextWriter writer = new XmlTextWriter(file, Encoding.UTF8) {
                Formatting = Formatting.Indented
            };
            writer.WriteStartDocument(true);
            writer.WriteStartElement("docktree");
            foreach (DockManager manager in managerList)
            {
                manager.WriteXml(writer);
            }
            foreach (DockForm form in formList)
            {
                form.WriteXml(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        internal override void WriteXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("manager");
            writer.WriteAttributeString("parent", base.Parent.GetType().FullName);
            foreach (DockContainer container in base.conList)
            {
                container.WriteXml(writer);
            }
            foreach (DockPanel panel in base.panList)
            {
                panel.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        internal static OverlayForm DockGuide
        {
            get
            {
                return dockGuide;
            }
            set
            {
                dockGuide = value;
            }
        }

        [Category("DockDotNET"), Description("获取或设置控制快速的标志，但不惊人的图纸时，移动窗口，以提高性能。"), DefaultValue(false)]
        public bool FastDrawing
        {
            get
            {
                return FastMoveDraw;
            }
            set
            {
                FastMoveDraw = value;
            }
        }

        public static bool FastMoveDraw
        {
            get
            {
                return fastMoveDraw;
            }
            set
            {
                fastMoveDraw = value;
            }
        }

        public static Collection<DockPanel> ListDocument
        {
            get
            {
                return listDocument;
            }
            set
            {
                listDocument = value;
            }
        }

        public static Collection<DockPanel> ListPanel
        {
            get
            {
                return listPanel;
            }
            set
            {
                listPanel = value;
            }
        }

        public static Collection<DockPanel> ListTool
        {
            get
            {
                return listTool;
            }
            set
            {
                listTool = value;
            }
        }

        internal static bool NoGuidePlease
        {
            get
            {
                return noGuidePlease;
            }
            set
            {
                noGuidePlease = value;
            }
        }

        public static DockVisualStyle Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
            }
        }

        [DefaultValue(1), Category("DockDotNET")]
        public DockVisualStyle VisualStyle
        {
            get
            {
                return Style;
            }
            set
            {
                Style = value;
            }
        }
    }
}

