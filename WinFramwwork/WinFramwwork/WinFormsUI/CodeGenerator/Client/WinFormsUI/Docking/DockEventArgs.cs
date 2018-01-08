namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.Drawing;

    public class DockEventArgs : EventArgs
    {
        private DockContainerType dockType;
        private bool handled = false;
        private bool ignoreHierarchy = false;
        private System.Drawing.Point point;
        private bool release;
        private bool showDockGuide = false;
        private DockContainer target = null;

        public DockEventArgs(System.Drawing.Point point, DockContainerType dockType, bool release)
        {
            this.point = point;
            this.dockType = dockType;
            this.release = release;
            this.target = null;
            this.handled = false;
            this.showDockGuide = false;
        }

        public DockContainerType DockType
        {
            get
            {
                return this.dockType;
            }
        }

        public bool Handled
        {
            get
            {
                return this.handled;
            }
            set
            {
                this.handled = value;
            }
        }

        public bool IgnoreHierarchy
        {
            get
            {
                return this.ignoreHierarchy;
            }
            set
            {
                this.ignoreHierarchy = value;
            }
        }

        public System.Drawing.Point Point
        {
            get
            {
                return this.point;
            }
            set
            {
                this.point = value;
            }
        }

        public bool Release
        {
            get
            {
                return this.release;
            }
        }

        public bool ShowDockGuide
        {
            get
            {
                return this.showDockGuide;
            }
            set
            {
                this.showDockGuide = value;
            }
        }

        public DockContainer Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }
    }
}

