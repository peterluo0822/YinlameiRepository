namespace CodeGenerator.Client.WinFormsUI.Controls
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class XListView : ListView
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETGROUPINFO = 0x1093;
        private const int WM_LBUTTONUP = 0x202;

        private static int? GetGroupID(ListViewGroup lstvwgrp)
        {
            int? nullable = null;
            System.Type type = lstvwgrp.GetType();
            if (type != null)
            {
                PropertyInfo property = type.GetProperty("ID", BindingFlags.NonPublic | BindingFlags.Instance);
                if (property == null)
                {
                    return nullable;
                }
                object obj2 = property.GetValue(lstvwgrp, null);
                if (obj2 != null)
                {
                    nullable = obj2 as int?;
                }
            }
            return nullable;
        }

        [Description("Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message. To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function."), DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        public void SetGroupFooter(ListViewGroup lvg, string footerText)
        {
            setGrpFooter(lvg, footerText);
        }

        public void SetGroupState(ListViewGroupState state)
        {
            foreach (ListViewGroup group in base.Groups)
            {
                setGrpState(group, state);
            }
        }

        public static void setGrpFooter(ListViewGroup lstvwgrp, string footer)
        {
            if ((Environment.OSVersion.Version.Major >= 6) && ((lstvwgrp != null) && (lstvwgrp.ListView != null)))
            {
                if (lstvwgrp.ListView.InvokeRequired)
                {
                    lstvwgrp.ListView.Invoke(new CallbackSetGroupString(XListView.setGrpFooter), new object[] { lstvwgrp, footer });
                }
                else
                {
                    CodeGenerator.Client.WinFormsUI.Controls.LVGROUP lvgroup=new LVGROUP();
                    int? groupID = GetGroupID(lstvwgrp);
                    int index = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp);
                    lvgroup = new CodeGenerator.Client.WinFormsUI.Controls.LVGROUP {
                        CbSize = Marshal.SizeOf(lvgroup),
                        PszFooter = footer,
                        Mask = ListViewGroupMask.Footer
                    };
                    IntPtr ptr = Marshal.AllocHGlobal(0xfa000);
                    if (groupID.HasValue)
                    {
                        lvgroup.IGroupId = groupID.Value;
                        Marshal.StructureToPtr(lvgroup, ptr, true);
                        SendMessage(lstvwgrp.ListView.Handle, 0x1093, groupID.Value, ptr);
                    }
                    else
                    {
                        lvgroup.IGroupId = index;
                        Marshal.StructureToPtr(lvgroup, ptr, true);
                        SendMessage(lstvwgrp.ListView.Handle, 0x1093, index, ptr);
                    }
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }

        public static void setGrpState(ListViewGroup lstvwgrp, ListViewGroupState state)
        {
            if ((Environment.OSVersion.Version.Major >= 6) && ((lstvwgrp != null) && (lstvwgrp.ListView != null)))
            {
                if (lstvwgrp.ListView.InvokeRequired)
                {
                    lstvwgrp.ListView.Invoke(new CallBackSetGroupState(XListView.setGrpState), new object[] { lstvwgrp, state });
                }
                else
                {
                    CodeGenerator.Client.WinFormsUI.Controls.LVGROUP lvgroup=new LVGROUP ();
                    int? groupID = GetGroupID(lstvwgrp);
                    int index = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp);
                    lvgroup = new CodeGenerator.Client.WinFormsUI.Controls.LVGROUP {
                        CbSize = Marshal.SizeOf(lvgroup),
                        State = state,
                        Mask = ListViewGroupMask.State
                    };
                    IntPtr ptr = Marshal.AllocHGlobal(0xfa000);
                    if (groupID.HasValue)
                    {
                        lvgroup.IGroupId = groupID.Value;
                        Marshal.StructureToPtr(lvgroup, ptr, true);
                        SendMessage(lstvwgrp.ListView.Handle, 0x1093, groupID.Value, ptr);
                    }
                    else
                    {
                        lvgroup.IGroupId = index;
                        lvgroup.IGroupId = groupID.Value;
                        SendMessage(lstvwgrp.ListView.Handle, 0x1093, index, ptr);
                    }
                    Marshal.FreeHGlobal(ptr);
                    lstvwgrp.ListView.Refresh();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x202)
            {
                base.DefWndProc(ref m);
            }
            base.WndProc(ref m);
        }

        private delegate void CallBackSetGroupState(ListViewGroup lstvwgrp, ListViewGroupState state);

        private delegate void CallbackSetGroupString(ListViewGroup lstvwgrp, string value);
    }
}

