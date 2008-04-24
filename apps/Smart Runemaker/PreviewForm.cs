using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Tibia;
using Tibia.Objects;

namespace SmartRunemaker
{
    public partial class PreviewForm : Form
    {
        private Client client;

        private IntPtr thumb;

        #region Constants

        static readonly int GWL_STYLE = -16;

        static readonly int DWM_TNP_VISIBLE = 0x8;
        static readonly int DWM_TNP_OPACITY = 0x4;
        static readonly int DWM_TNP_RECTDESTINATION = 0x1;

        static readonly ulong WS_VISIBLE = 0x10000000L;
        static readonly ulong WS_BORDER = 0x00800000L;
        static readonly ulong TARGETWINDOW = WS_BORDER | WS_VISIBLE;

        #endregion

        #region DWM functions

        [DllImport("dwmapi.dll")]
        static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll")]
        static extern int DwmUnregisterThumbnail(IntPtr thumb);

        [DllImport("dwmapi.dll")]
        static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out PSIZE size);

        [DllImport("dwmapi.dll")]
        static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        #endregion
        
        public PreviewForm()
        {
            InitializeComponent();
        }

        public void showPreview(Client c)
        {
            client = c;
            this.Show();
            int i = DwmRegisterThumbnail(this.Handle, client.Process.MainWindowHandle, out thumb);
            UpdateThumb();
        }

        private void UpdateThumb()
        {
            if (thumb != IntPtr.Zero)
            {
                PSIZE size;
                DwmQueryThumbnailSourceSize(thumb, out size);

                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();

                props.fVisible = true;
                props.dwFlags = DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY;
                props.opacity = (byte)255;//opacity.Value;
                props.rcDestination = new Rect(image.Left, image.Top, image.Right, image.Bottom);

                if (size.x < image.Width)
                    props.rcDestination.Right = props.rcDestination.Left + size.x;

                if (size.y < image.Height)
                    props.rcDestination.Bottom = props.rcDestination.Top + size.y;

                DwmUpdateThumbnailProperties(thumb, ref props);
            }
        }

        private void PreviewForm_Resize(object sender, EventArgs e)
        {
            UpdateThumb();
        }

        private bool mouse_is_down = false;
        private Point mouse_pos;

        private void image_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouse_is_down = true;
                    mouse_pos.X = e.X;
                    mouse_pos.Y = e.Y;
                    break;
                case MouseButtons.Middle:
                    client.IsActive = true;
                    break;
            }
        }

        private void image_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_is_down = false;
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_is_down)
            {
                Point current_pos = Control.MousePosition;
                current_pos.X = current_pos.X - mouse_pos.X; //add this 
                current_pos.Y = current_pos.Y - mouse_pos.Y; //add this
                this.Location = current_pos;
            }
        }

        #region Interop structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public Rect rcDestination;
            public Rect rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            internal Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PSIZE
        {
            public int x;
            public int y;
        }

        #endregion
    }
}