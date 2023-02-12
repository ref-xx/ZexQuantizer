using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ZeXQuantizer
{
    class FastPixel
    {
        private byte[] rgbValues;
        private BitmapData bmpData;
        private bool locked = false;

        private bool _isAlpha = false;
        private Bitmap _bitmap;
        private int _width;
        private int _height;
        private int bytesPerPixel;

        public int Width
        {
            get { return this._width; }
        }

        public int Height
        {
            get { return this._height; }
        }

        public bool IsAlphaBitmap
        {
            get { return this._isAlpha; }
        }

        public Bitmap Bitmap
        {
            get { return this._bitmap; }
        }

        public FastPixel(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == (bitmap.PixelFormat | PixelFormat.Indexed))
                throw new Exception("Cannot lock an Indexed image.");

            this._bitmap = bitmap;
            this._isAlpha = (this.Bitmap.PixelFormat == (this.Bitmap.PixelFormat | PixelFormat.Alpha));
            this._width = bitmap.Width;
            this._height = bitmap.Height;
            this.bytesPerPixel = (this._isAlpha) ? 4 : 3;
        }

        public void Lock()
        {
            if (this.locked)
                throw new Exception("Bitmap already locked.");

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            this.bmpData = this.Bitmap.LockBits(rect, ImageLockMode.ReadWrite, this.Bitmap.PixelFormat);
            this.rgbValues = new byte[(this.Width * this.Height) * this.bytesPerPixel];

            unsafe
            {
                byte* ptr = (byte*)this.bmpData.Scan0;
                int offset = this.bmpData.Stride - this.bmpData.Width * this.bytesPerPixel;
                for (int y = 0; y < this.Height; y++, ptr += offset)
                {
                    for (int x = 0; x < this.Width; x++, ptr += this.bytesPerPixel)
                    {
                        int index = ((y * this.Width + x) * this.bytesPerPixel);
                        this.rgbValues[index] = ptr[0];
                        this.rgbValues[index + 1] = ptr[1];
                        this.rgbValues[index + 2] = ptr[2];
                        if (this.bytesPerPixel == 4) this.rgbValues[index + 3] = ptr[3];
                    }
                }
            }
            this.locked = true;
        }

        public void Unlock(bool setPixels)
        {
            if (!this.locked)
                throw new Exception("Bitmap not locked.");

            // Copy the RGB values back to the bitmap;
            if (setPixels)
            {
                unsafe
                {
                    byte* ptr = (byte*)this.bmpData.Scan0;
                    int offset = this.bmpData.Stride - this.bmpData.Width * this.bytesPerPixel;
                    for (int y = 0; y < this.Height; y++, ptr += offset)
                    {
                        for (int x = 0; x < this.Width; x++, ptr += this.bytesPerPixel)
                        {
                            int index = ((y * this.Width + x) * this.bytesPerPixel);
                            ptr[0] = this.rgbValues[index];
                            ptr[1] = this.rgbValues[index + 1];
                            ptr[2] = this.rgbValues[index + 2];
                            if (this.bytesPerPixel == 4) ptr[3] = this.rgbValues[index + 3];
                        }
                    }
                }
            }

            // Unlock the bits.;
            this.Bitmap.UnlockBits(bmpData);
            this.locked = false;
        }

        public void Clear(Color colour)
        {
            if (!this.locked)
                throw new Exception("Bitmap not locked.");

            for (int index = 0; index < this.rgbValues.Length; index += this.bytesPerPixel)
            {
                this.rgbValues[index] = colour.B;
                this.rgbValues[index + 1] = colour.G;
                this.rgbValues[index + 2] = colour.R;
                if (this.bytesPerPixel == 4) this.rgbValues[index + 3] = colour.A;
            }
        }

        public void SetPixel(Point location, Color colour)
        {
            this.SetPixel(location.X, location.Y, colour);
        }

        public void SetPixel(int x, int y, Color colour)
        {
            if (!this.locked)
                throw new Exception("Bitmap not locked.");

            int index = ((y * this.Width + x) * this.bytesPerPixel);
            this.rgbValues[index] = colour.B;
            this.rgbValues[index + 1] = colour.G;
            this.rgbValues[index + 2] = colour.R;
            if (this.bytesPerPixel == 4) this.rgbValues[index + 3] = colour.A;
        }


        public void FillRect(int x, int y, int w, int h, Color colour)
        {
            for (int ry = y; ry < y + h; ry++)
                for (int rx = x; rx < x + w; rx++)
                {
                    SetPixel(rx, ry, colour);
                }

        }
        public void ThinRect(int x, int y, int w, int h, Color colour, Color colour2)
        {
            //Color colour2 = Color.White;
            bool flipflop = true;
            for (int ry = y; ry < y + h; ry++)
                for (int rx = x; rx < x + w; rx++)
                {
                    if ((rx == x) || (ry == y) || (rx == x + w - 1) || (ry == y + h - 1))
                    { 
                        if (flipflop) SetPixel(rx, ry, colour); else  SetPixel(rx, ry, colour2);
                        
                    }
                    flipflop = !flipflop;
                }

        }

        public Color GetPixel(Point location)
        {
            return this.GetPixel(location.X, location.Y);
        }

        public Color GetPixel(int x, int y)
        {
            if (!this.locked)
                throw new Exception("Bitmap not locked.");

            int index = ((y * this.Width + x) * this.bytesPerPixel);
            int b = this.rgbValues[index];
            int g = this.rgbValues[index + 1];
            int r = this.rgbValues[index + 2];
            if (this.bytesPerPixel == 4)
            {
                int a = this.rgbValues[index + 3];
                return Color.FromArgb(a, r, g, b);
            }
            return Color.FromArgb(r, g, b);
        }
    }
}
