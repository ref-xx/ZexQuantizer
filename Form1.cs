using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ZeXQuantizer
{
    public partial class Form1 : Form
    {
        Color[] pixel = new Color[16];
        private Image imgOriginal = new Bitmap(256, 192);
        int bright = 251;
        int nobright = 200;
        string integ = "";
        int lastresize = 2;
        int[,] marks = new int[8, 8]; //holds attributes as 0..15 zx colors
        int[,] cell = new int[8, 8]; //this is zoomed in picture for cell
        int[,] cellUndo = new int[8, 8];
        int PenColor = -1; //a palette index for paint
        int PaperColor = -1;
        int EditingX = -1;
        int EditingY = -1;
        List<int> ColorList = new List<int>(); //holds cell palette indexes
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.DoubleBuffered = true;

        }
        private void setpalette(bool noBright = false)
        {
            int brightblack = 16;// bright - nobright;
            pixel[0] = Color.FromArgb(255, 0, 0, 0);
            pixel[1] = Color.FromArgb(255, 0, 0, nobright);
            pixel[2] = Color.FromArgb(255, nobright, 0, 0);
            pixel[3] = Color.FromArgb(255, nobright, 0, nobright);
            pixel[4] = Color.FromArgb(255, 0, nobright, 0);
            pixel[5] = Color.FromArgb(255, 0, nobright, nobright);
            pixel[6] = Color.FromArgb(255, nobright, nobright, 0);
            pixel[7] = Color.FromArgb(255, nobright, nobright, nobright);


            pixel[8] = Color.FromArgb(255, brightblack, brightblack, brightblack);
            pixel[9] = Color.FromArgb(255, brightblack, brightblack, bright);
            pixel[10] = Color.FromArgb(255, bright, brightblack, brightblack);
            pixel[11] = Color.FromArgb(255, bright, brightblack, bright);
            pixel[12] = Color.FromArgb(255, brightblack, bright, brightblack);
            pixel[13] = Color.FromArgb(255, brightblack, bright, bright);
            pixel[14] = Color.FromArgb(255, bright, bright, brightblack);
            pixel[15] = Color.FromArgb(255, bright, bright, bright);

            if (noBright)
            {
                pixel[8] = Color.FromArgb(255, 0, 0, 0);
                pixel[9] = Color.FromArgb(255, 0, 0, nobright);
                pixel[10] = Color.FromArgb(255, nobright, 0, 0);
                pixel[11] = Color.FromArgb(255, nobright, 0, nobright);
                pixel[12] = Color.FromArgb(255, 0, nobright, 0);
                pixel[13] = Color.FromArgb(255, 0, nobright, nobright);
                pixel[14] = Color.FromArgb(255, nobright, nobright, 0);
                pixel[15] = Color.FromArgb(255, nobright, nobright, nobright);
            }

        }
        private Bitmap Quantize(Image pic, bool invalidOnly=false)
        {
            int h = 8;
            if (rdTimex.Checked) h = 1;

            int isMLT = 8;
            if (marks.GetLength(1) == 192) isMLT = 1;

            setpalette(chkNoBright.Checked);

            Bitmap marked = new Bitmap(pic);
            FastPixel fpm = new FastPixel(marked);
            fpm.Lock();
            int counter = 0;

            //convert all pixels to nearest zxspectrum palette colors
            int[,] PixelTable = new int[marked.Width, marked.Height];
            for (int x = 0; x < marked.Width; x++)
                for (int y = 0; y < marked.Height; y++)
                {
                    if ((x == 7) && (y == 7))
                    {
                        counter++;
                    }
                    Color PixelColor = fpm.GetPixel(x, y);
                    double maxdist = 9999;
                    int selectedColor = 16;
                    for (int i = 0; i < 16; i++)
                    {
                        double dist = GetDistance(PixelColor, pixel[i]);
                        if (maxdist > dist)
                        {
                            maxdist = dist;
                            selectedColor = i;
                        }
                    }
                    if (selectedColor != 16)
                    {
                        fpm.SetPixel(x, y, pixel[selectedColor]);
                        PixelTable[x, y] = selectedColor;
                    }
                    else
                    {
                        //throw error
                        Console.WriteLine("ERROR");
                    }
                }
            //DONE --but no color clash is checked
            if (!rdProcClash.Checked && !rdTimex.Checked)
            {
                fpm.Unlock(true);
                return marked;
            }
            //Clash check requested

            //Find Most Popular 2 Colors
            int[] popPalet = new int[16];
            for (int x = 0; x < marked.Width; x = x + 8)
                for (int y = 0; y < marked.Height; y = y + h) //if timex checked h=1 else 8
                {
                    //PROCESS one block 8x8 or 8x1 now
                    bool processThisBlock = true;
                    if (invalidOnly)
                    {
                        if (marks[x / 8, y / isMLT] != -1) processThisBlock = false;
                    }

                    if (processThisBlock)
                    {
                        Array.Clear(popPalet, 0, 16);
                        for (int ix = 0; ix < 8; ix++)
                            for (int iy = 0; iy < h; iy++)
                            {
                                popPalet[PixelTable[x + ix, y + iy]]++;
                            }
                        int popular = 16;

                        int count = -1;
                        for (int r = 0; r < 16; r++)
                        {
                            if (popPalet[r] > count)
                            {
                                popular = r;
                                count = popPalet[r];
                            }
                        }
                        int lastpopular = 16;
                        count = -1;
                        for (int r = 0; r < 16; r++)
                        {
                            if (r != popular)
                            {
                                if (popPalet[r] > count)
                                {

                                    lastpopular = r;
                                    count = popPalet[r];
                                }
                            }
                        }
                        //adjust brightness
                        if ((popular > 7 && lastpopular < 8) ||
                                (popular < 8 && lastpopular > 7))
                        {
                            if (popPalet[popular] > popPalet[lastpopular])
                            {
                                if (popular > 7)
                                {
                                    lastpopular += 8;
                                }
                                else
                                {
                                    lastpopular -= 8;
                                }
                            }
                            else
                            {
                                if (lastpopular > 7)
                                {
                                    popular += 8;
                                }
                                else
                                {
                                    popular -= 8;
                                }
                            }
                        }

                        Color[] ColorTable = new Color[2];
                        ColorTable[0] = pixel[popular];
                        ColorTable[1] = pixel[lastpopular];
                        for (int ix = 0; ix < 8; ix++)
                            for (int iy = 0; iy < h; iy++)
                            {
                                Color PixelColor = fpm.GetPixel(x + ix, y + iy);
                                double maxdist = 9999;
                                int selectedColor = 16;
                                for (int i = 0; i < 2; i++)
                                {
                                    double dist = GetDistance(PixelColor, ColorTable[i]);
                                    if (maxdist > dist)
                                    {
                                        maxdist = dist;
                                        selectedColor = i;
                                    }
                                }
                                if (selectedColor != 16)
                                {
                                    fpm.SetPixel(x + ix, y + iy, ColorTable[selectedColor]);
                                }

                            }

                    }
                    //END OF BLOCK CHECKING
                }
            fpm.Unlock(true);
            return marked;
        }

        private Double GetDistance(Color Source, Color Preset)
        {
            double dbl_test_red = Math.Pow(Convert.ToDouble(Preset.R) - Convert.ToDouble(Source.R), 2.0);
            double dbl_test_green = Math.Pow(Convert.ToDouble(Preset.G) - Convert.ToDouble(Source.G), 2.0);
            double dbl_test_blue = Math.Pow(Convert.ToDouble(Preset.B) - Convert.ToDouble(Source.B), 2.0);

            double dist = Math.Sqrt(dbl_test_blue + dbl_test_green + dbl_test_red);
            //Math.Sqrt((Source.R - Preset.R) ^ 2+ (Source.G - Preset.G) ^ 2+ (Source.B - Preset.B) ^ 2);

            return dist;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //LOAD IMAGE
            
            if (!File.Exists(txtFilename.Text))
            {
                txtFilename.Text="File not found.";
                return;
            }

            picSource.Image = Image.FromFile(txtFilename.Text);
            //picSource.Width = picSource.Image.Width;
            //picSource.Height = picSource.Image.Height;
            imgOriginal = picSource.Image;
            Image realimage = copyImage(imgOriginal);
            picOriginal.Image = realimage;
            //picSource.Image = imgOriginal;
            //setDestPos(imgOriginal);
            chkInvalids.Checked = false;
            chkInvalids.Enabled = false;
            chkQuantizeInvalids.Checked = false;
            if ((imgOriginal.Width >= 1023)||(imgOriginal.Height >= 767)) lastresize = 1;
            zoomSource(lastresize);
            if ((picSource.Width / 8f) != (int)(picSource.Width / 8))
            {
                integ = "Not integer";

            }
            label1.Text = picSource.Width.ToString() + "x" + picSource.Height.ToString() + " :" + integ;

        }

        private void openFile()
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image File";
            theDialog.Filter = "All files|*.*";
            //theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {

                picSource.Image = Image.FromFile(theDialog.FileName);
                picSource.Width = picSource.Image.Width;
                picSource.Height = picSource.Image.Height;
                imgOriginal = picSource.Image;
                marks = new int[picSource.Image.Width / 8, picSource.Image.Height / 8];
                //picSource.Image = imgOriginal;
                //setDestPos(imgOriginal);
                chkInvalids.Checked = false;
                chkInvalids.Enabled = false;
                chkQuantizeInvalids.Checked = false;
                zoomSource(lastresize);
                Image realimage=copyImage(imgOriginal);
                picOriginal.Image = realimage;
                txtFilename.Text = theDialog.FileName;
                if ((picSource.Width / 8f) != (int)(picSource.Width / 8))
                {
                    integ = "Not integer";
                    rdFree.Checked = true;
                }
                label1.Text = picSource.Width.ToString() + "x" + picSource.Height.ToString() + " :" + integ;

            }

        }
        private void setDestPos(Image img)
        {
            picDest.Top = picSource.Top;
            picDest.Left = picSource.Left + picSource.Width + 8;
            picDest.Height = img.Height;
            picDest.Width = img.Width;
            picDest.Image = img;
            button2.Enabled = true;
            integ = "";
            
            //if (ClientSize.Height < picSource.Top + picSource.Height)
            {
                Form1.ActiveForm.Height += 8 + (picSource.Top + picSource.Height) - ClientSize.Height;

            }
            //if (ClientSize.Width < picDest.Width + picDest.Left)
            {
                Form1.ActiveForm.Width += 8 + (picDest.Width + picDest.Left) - ClientSize.Width;

            }
            if (this.Size.Width > Screen.PrimaryScreen.WorkingArea.Size.Width)
            {
                //lastresize--;
                //zoomSource(lastresize);
                Application.DoEvents();
            }
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            imgOriginal = Quantize(imgOriginal,chkQuantizeInvalids.Checked);
            bool needswork = false;
            marks = checkMismatch(imgOriginal, out needswork);

            if (!needswork)
            {
                button8.Enabled = true;
                if (marks.GetLength(1) == 192)
                {
                    button8.Text = "3.Save As Mlt...";
                }
                else
                { button8.Text = "3.Save As Scr..."; }
            }
            else
            {
                button8.Enabled = false;
            }

            chkInvalids.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            zoomSource(lastresize);
            picSource.Refresh();
        }




        private void button3_Click(object sender, EventArgs e)
        {
            zoomSource(2);

        }

        private void picSource_Click(object sender, EventArgs e)
        {

        }

        private void picSource_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (imgOriginal != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(
                      imgOriginal,
                       new Rectangle(0, 0, picSource.Width, picSource.Height),
                       // destination rectangle 
                       0,
                       0,           // upper-left corner of source rectangle
                       imgOriginal.Width,       // width of source rectangle
                       imgOriginal.Height,      // height of source rectangle
                       GraphicsUnit.Pixel);
            
                }
            */
        }

        private void button4_Click(object sender, EventArgs e)
        {

            zoomSource(3);

        }
        private void zoomSource(int factor)
        {
            picSource.SizeMode = PictureBoxSizeMode.CenterImage;
            picSource.Width = imgOriginal.Width * factor;
            picSource.Height = imgOriginal.Height * factor;
            picSource.Image = resize(imgOriginal, factor);
            setDestPos(imgOriginal);

        }

        private Bitmap copyImage(Image originalImage)
        {
            // Create a new Bitmap object with the same size as the original image
            Bitmap copyImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Create a Graphics object from the new Bitmap object
            using (Graphics graphics = Graphics.FromImage(copyImage))
            {
                // Draw the original image onto the new Bitmap object
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));
            }
            return copyImage;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zoomSource(1);
        }

        private int[,] checkMismatch(Image pic, out bool needswork)
        {
            int mltOrScr = 8;
            if (rdTimex.Checked) mltOrScr = 1;
            int[,] nmarks = new int[pic.Width / 8, pic.Height / mltOrScr];
            Bitmap inpic = new Bitmap(pic);
            FastPixel fpi = new FastPixel(inpic);
            fpi.Lock();
            Color[] cols = new Color[2];

            bool invalid = false;
            needswork = false;
            for (int x = 0; x < inpic.Width; x += 8)
                for (int y = 0; y < inpic.Height; y += mltOrScr)
                {
                    cols[0] = Color.Pink;
                    cols[1] = Color.Pink;
                    invalid = false;
                    for (int rsy = 0; rsy < mltOrScr; rsy++)
                    {
                        for (int rsx = 0; rsx < 8; rsx++)
                        {
                            Color c = fpi.GetPixel(x + rsx, y + rsy);
                            int idx = findCol(c);
                            if (idx >= 0)
                            {
                                //color exists


                                if ((cols[0] == c) || (cols[1] == c))
                                {
                                    //not a new color but it's a valid pre-stored color
                                }
                                else
                                {
                                    // a new color
                                    // check if any free ink or paper for 8x8block
                                    if (cols[0] == Color.Pink)
                                    {
                                        cols[0] = c;
                                        nmarks[x / 8, y / mltOrScr] = idx;
                                    }
                                    else if (cols[1] == Color.Pink)
                                    {
                                        cols[1] = c;
                                        nmarks[x / 8, y / mltOrScr] += idx * 256;
                                    }
                                    else
                                    {
                                        //out of free slots
                                        //we have a problem. mark this block and move to next
                                        invalid = true;
                                        needswork = true;
                                        nmarks[x / 8, y / mltOrScr] = -1;
                                    }
                                }

                            }
                            if (invalid) break; //exit inner x loop
                        }
                        if (invalid)
                        {
                            break; //exit outer y loop
                        }
                    }

                }
            if (needswork)
            {
                if (mltOrScr == 8) label1.Text = "INVALID SCR"; else label1.Text = "INVALID MLT";
            }
            else
            {
                button8.Enabled = true;

                if (mltOrScr == 8) label1.Text = "GOOD SCR"; else label1.Text = "GOOD MLT";
            }

            return nmarks;
        }

        private int findCol(Color c)
        {
            for (int x = 0; x < 16; x++)
            {
                if (c == pixel[x]) return x;
            }
            return -1;
        }
        
        private Bitmap resize(Image pic, int factor)
        {
            int scrormlt = 8;
            if (marks.GetLength(1) == 192) scrormlt = 1;
            lastresize = factor;
            int ygrid = 8;
            int xgrid = 8;
            if (chkGrid.Checked)
            {

                ygrid = scrormlt;
            }
            else
            {
                ygrid = 0;
            }



            Color g = Color.FromArgb(0xFF, 0x00, 0x78, 0xd7);
            Color e = Color.Red;// Color.FromArgb(0xFF, 0x78, 0x00, 0xd7);


            Bitmap inpic = new Bitmap(pic);
            Bitmap result = new Bitmap(pic.Width * factor, pic.Height * factor);
            FastPixel fpr = new FastPixel(result);
            FastPixel fpi = new FastPixel(inpic);
            fpr.Lock();
            fpi.Lock();
            int yfac = ygrid * factor;
            int xfac = xgrid * factor;
            for (int y = 0; y < result.Height; y += factor)
                for (int x = 0; x < result.Width; x += factor)
                {
                    Color c = fpi.GetPixel(x / factor, y / factor);
                    for (int rsy = 0; rsy < factor; rsy++) 
                    {
                        for (int rsx = 0; rsx < factor; rsx++)
                        {
                            if ((ygrid > 0) && ((y % xfac == 0) || (x % (xfac) == 0)) && (rsy == 0 && rsx == 0))
                                fpr.SetPixel(rsx + x, rsy + y, g);
                            else
                            {
                                fpr.SetPixel(rsx + x, rsy + y, c);
                                //Console.WriteLine((y % (ygrid * factor)).ToString() + "**" + y.ToString() + "*g" + ygrid.ToString());

                            }
                        }
                    }
                    
                }
            
            if (chkInvalids.Checked)// || chkGrid.Checked)
            for (int y=0;y<marks.GetLength(1);y++)
                for (int x = 0; x < marks.GetLength(0); x++)
                {
                    if (chkInvalids.Checked && (marks[x, y] == -1))
                    {
                        fpr.ThinRect(x*8 * factor, y *scrormlt* factor, 8 * factor, scrormlt * factor, e,g);
                    }
                    else
                    {
                       // if ((scrormlt==1)&&(chkGrid.Checked)) fpr.ThinRect(x *8 * factor, y*scrormlt * factor, 1+8 * factor, 1+scrormlt * factor, g);
                    }
                }

            fpr.Unlock(true);
            fpi.Unlock(true);
            return result;


        }

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            zoomSource(lastresize);
        }

        private void chkInvalids_CheckedChanged(object sender, EventArgs e)
        {
            zoomSource(lastresize);
        }

        private void picSource_MouseClick(object sender, MouseEventArgs e)
        {

            int scrormlt = 8;
            if (marks.GetLength(1) == 192) scrormlt = 1; else if (marks.GetLength(1) != 24) return;

            int row = (e.X / lastresize) / 8;
            int col = (e.Y / lastresize) / scrormlt;
            int ink = marks[row, col] / 256;
            int paper = marks[row, col] % 256;
            string sbright = " No Bright";
            if (ink > 7 || paper > 7)
            {
                if (ink != 0) ink -= 8;
                paper -= 8;
                sbright = " Bright";
            }

            label1.Text = row.ToString() + "x" + col.ToString() + " INK" + (ink).ToString() + " PAPER" + (paper).ToString() + sbright;
            selectCell(row, col);

        }


        private void selectCell(int row, int col)
        {
            int scrormlt = 8;
            if (marks.GetLength(1) == 192) scrormlt = 1; else if (marks.GetLength(1) != 24) return;

            bool isZX = false;
            cell = getCell(out isZX, imgOriginal, row, col, scrormlt);
            cellUndo = new int[8, scrormlt];
            Array.Copy(cell, cellUndo, cell.Length);
            if (isZX)
            {
                refreshCellPic();
                EditingX = row;
                EditingY = col;
                chkValidCell.Checked = CheckInvalid(cell);
            }
        }
        private void refreshCellPic(bool UpdateMainScreen = false)
        {
            picCell.Image = paintCell(cell, picCell.Width, picCell.Height);
            if (UpdateMainScreen)
            {
                imgOriginal = mergeCellToMain(EditingX, EditingY, cell, imgOriginal);
                zoomSource(lastresize);
            }
        }
        private Image mergeCellToMain(int CellLocX, int CellLocY, int[,] cell, Image pic)
        {
            int isMLT = cell.GetLength(1);


            Bitmap cellView = new Bitmap(pic.Width, pic.Height);
            using (Graphics g = Graphics.FromImage(cellView))
            {
                // Draw the contents of the original image onto the new Bitmap object
                g.DrawImage(pic, new Rectangle(0, 0, pic.Width, pic.Height));
            }

            FastPixel fpi = new FastPixel(cellView);
            fpi.Lock();
            for (int y = 0; y < isMLT; y++)
                for (int x = 0; x < 8; x++)
                {
                    fpi.SetPixel(CellLocX * 8 + x, CellLocY * isMLT + y, pixel[cell[x, y]]);
                }
            fpi.Unlock(true);
            return cellView;

        }
        private Image paintCell(int[,] cell, int w, int h, int selectedColorIndex = -1, int zoomLevel = 16)
        {
            int isMLT = cell.GetLength(1);
            Bitmap cellView = new Bitmap(w, h);
            FastPixel fpi = new FastPixel(cellView);
            fpi.Lock();
            ColorList.Clear();

            for (int y = 0; y < isMLT; y++)
                for (int x = 0; x < 8; x++)
                {
                    if (!ColorList.Contains(cell[x, y])) ColorList.Add(cell[x, y]);
                    fpi.FillRect(x * zoomLevel, y * zoomLevel, zoomLevel, zoomLevel, pixel[cell[x, y]]);
                    if (selectedColorIndex == cell[x, y]) fpi.ThinRect(x * zoomLevel, y * zoomLevel, zoomLevel, zoomLevel, Color.FromArgb(0xFF, 0x00, 0x78, 0xd7), Color.FromArgb(0xFF, 0x00, 0x78, 0xd7));
                }


            for (int x = 0; x < ColorList.Count; x++)
            {
                fpi.FillRect(x * zoomLevel, h - zoomLevel, zoomLevel - 1, zoomLevel - 1, pixel[ColorList[x]]);

            }

            for (int x = 0; x < 8; x++)
            {
                fpi.FillRect(x * zoomLevel, h - zoomLevel * 3 - 1, zoomLevel - 1, zoomLevel, pixel[x]);

            }
            for (int x = 8; x < 16; x++)
            {
                fpi.FillRect((x - 8) * zoomLevel, h - zoomLevel * 2 - 1, zoomLevel - 1, zoomLevel, pixel[x]);

            }
            fpi.ThinRect(0, h - zoomLevel - 1, zoomLevel * (ColorList.Count > 8 ? 8 : ColorList.Count), zoomLevel - 1,  Color.FromArgb(0xFF, 0x78, 0x78, 0xd7),Color.FromArgb(0xFF, 0x00, 0x78, 0xd7));

            fpi.ThinRect(0, isMLT * zoomLevel, w, 1, Color.Black, Color.Black);//a seperator

            fpi.Unlock(true);
            return cellView;
        }

        private int[,] getCell(out bool isZX, Image pic, int row, int col, int isMLT = 8)
        {
            isZX = true;
            int[,] cell = new int[8, isMLT];
            Bitmap inpic = new Bitmap(pic);
            FastPixel fpi = new FastPixel(inpic);
            fpi.Lock();
            for (int y = 0; y < isMLT; y++)
                for (int x = 0; x < 8; x++)
                {
                    int c = findCol(fpi.GetPixel((row * 8) + x, (col * isMLT) + y));
                    if (c >= 0)
                    {
                        cell[x, y] = c;
                    }
                    else
                    {
                        isZX = false;
                        fpi.Unlock(false);
                        return cell;
                    }

                }

            fpi.Unlock(false);
            return cell;
        }

        private void SavePNG(Image pic)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"; // set file type filters
            saveFileDialog.Title = "Save a PNG file"; // set dialog title
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {

                string selectedFileName = saveFileDialog.FileName;
                pic.Save(selectedFileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
        private void saveSCR(Image pic, bool isMLT)
        {

            bool needswork = false;
            marks = checkMismatch(pic, out needswork);

            if (pic.Width != 256 && pic.Height != 192 && needswork)
            {
                label1.Text = "Not a ZX Image. Check size";
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SCR files (*.scr)|*.scr|MLT files (*.mlt)|*.mlt|All files (*.*)|*.*"; // set file type filters
            saveFileDialog.Title = "Save a ZX Spectrum Screen file"; // set dialog title
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {

                string selectedFileName = saveFileDialog.FileName;
                //Console.WriteLine(selectedFileName);

                Bitmap inpic = new Bitmap(pic);
                FastPixel fpi = new FastPixel(inpic);
                fpi.Lock();
                int scrormlt = 768;
                if (isMLT) scrormlt = 6144;
                byte[] linearBuffer = new byte[6144]; // assume this is the linear buffer
                byte[] screenBuffer = new byte[6144]; // assume this is the non-linear screen buffer
                byte[] attrib = new byte[scrormlt];


                scrormlt = 8;
                if (isMLT) scrormlt = 1;
                int k = 0;
                for (int y = 0; y < 192; y++)
                {
                    for (int x = 0; x < 256; x += 8)
                    {
                        byte sb = 0;
                        for (int i = 0; i < 8; i++)
                        {


                            if (marks[(x + i) / 8, y / scrormlt] / 256 == findCol(fpi.GetPixel(x + i, y)))
                            {
                                sb |= (byte)(1 << (7 - i));
                            }

                        }

                        linearBuffer[k] = sb;
                        k++;
                    }
                }



                for (int l = 0; l < 3; l++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 32; x++)
                            {
                                screenBuffer[x + (y * 256) + (j * 32) + (l * 2048)] = linearBuffer[x + (y * 32) + (j * 256) + (l * 2048)];
                            }
                        }
                    }
                }

                scrormlt = 24;
                if (isMLT) scrormlt = 192;
                for (int y = 0; y < scrormlt; y++)
                {
                    for (int x = 0; x < 32; x++)
                    {
                        byte ink = (byte)(marks[x, y] / 256);
                        byte paper = (byte)(marks[x, y] % 256);
                        byte bri = 0;
                        if (ink > 7 || paper > 7)
                        {
                            if (ink != 0) ink -= 8;
                            paper -= 8;
                            bri = 1;
                        }
                        byte attribute = (byte)((paper << 3) | (ink << 0) | (bri << 6));
                        attrib[y * 32 + x] = attribute;
                    }
                }
                byte[] scr = screenBuffer.Concat(attrib).ToArray();



                File.WriteAllBytes(selectedFileName, scr);
            }

        }





        private void button7_Click(object sender, EventArgs e)
        {
            zoomSource(Convert.ToInt32(txtScale.Text));

        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool isMLT = false;
            if (marks.GetLength(1) == 192) isMLT = true;
            
            saveSCR(imgOriginal, isMLT);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            rdTimex.Checked = true;
            bool needswork = false;
            marks = checkMismatch(imgOriginal, out needswork);
            if (!needswork) button8.Text = "3.Save As Mlt...";
            else
                
                button8.Enabled = false;
            zoomSource(lastresize);
        }

        private void rdProcClash_CheckedChanged(object sender, EventArgs e)
        {
            if (integ != "" && rdProcClash.Checked)
            {
                button2.Enabled = false; //else button2.Enabled = true;
                button9.Enabled = false;
            }
        }

        private void rdTimex_CheckedChanged(object sender, EventArgs e)
        {
            if (integ != "" && rdTimex.Checked)
            {
                button2.Enabled = false; //else 
                button9.Enabled = false;
            }
        }

        private void rdFree_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //rdTimex.Checked = false;
            rdProcClash.Checked = true;
            bool needswork = false;
            marks = checkMismatch(imgOriginal, out needswork);
            if (!needswork) button8.Text = "3.Save As Scr...";
            else
                button8.Enabled = false;
            zoomSource(lastresize);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            imgOriginal = cropTo(256, 192, imgOriginal);
            zoomSource(lastresize);

        }
        private Image cropTo(int w, int h, Image pic)
        {
            Bitmap inpic = new Bitmap(pic);
            Bitmap result = new Bitmap(w, h);
            FastPixel fpr = new FastPixel(result);
            FastPixel fpi = new FastPixel(inpic);
            fpr.Lock();
            fpi.Lock();
            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                {
                    if ((y < inpic.Height) && (x < inpic.Width)) fpr.SetPixel(x, y, fpi.GetPixel(x, y));

                }
            fpi.Unlock(false);
            fpr.Unlock(true);
            return result;

        }

        private void picCell_MouseClick(object sender, MouseEventArgs e)
        {

            int zoomlevel = 16;
            int x = e.X / zoomlevel;
            int y = e.Y / zoomlevel;
            
            if (y < 8)
            {
                if (e.Button == MouseButtons.Right)
                {
                    //select all colors
                    PaperColor = cell[x, y];
                    picCell.Image = paintCell(cell, picCell.Width, picCell.Height, PaperColor);
                    PenColor = PaperColor;
                    picCell.BackColor = pixel[PenColor];
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (PenColor > -1)
                    {
                        //set a color --paint
                        cell[x, y] = PenColor;
                        PaperColor = -1;
                        picCell.Image = paintCell(cell, picCell.Width, picCell.Height);
                        chkValidCell.Checked = IsValidCell();

                    }
                    else
                    {
                        //no color is selected, deselect everything
                        PaperColor = -1;
                        picCell.Image = paintCell(cell, picCell.Width, picCell.Height);
                    }

                }
            }
            else if (y == 8)
            {
                if (PaperColor > -1)
                {
                    //There is a selected color, so we can replace a color
                    int replaceCol = x;
                    replaceColor(PaperColor, replaceCol);
                    refreshCellPic();
                    PaperColor = -1;
                    chkValidCell.Checked = IsValidCell();
                }
                else
                {
                    PenColor = x;
                    picCell.BackColor = pixel[PenColor];
                }
            }
            else if (y == 9)
            {
                if (PaperColor > -1)
                {
                    //There is a selected color, so we can replace a color
                    int replaceCol = x+8;
                    replaceColor(PaperColor, replaceCol);
                    refreshCellPic();
                    PaperColor = -1;
                    chkValidCell.Checked = IsValidCell();
                }
                else
                {
                    PenColor = x + 8;
                    picCell.BackColor = pixel[PenColor];
                }
            }
            //bottom part clicked
            else
            {


                {
                    //select color from index area
                    if (e.Button == MouseButtons.Left)
                    {


                        if (PaperColor > -1)
                        {
                            //There is a selected color, so we can replace a color
                            int replaceCol = ColorList[x];
                            replaceColor(PaperColor, replaceCol);
                            refreshCellPic();
                            PaperColor = -1;
                            chkValidCell.Checked = IsValidCell();
                        }
                        else
                        {
                            PenColor = ColorList[x];
                            picCell.BackColor = pixel[PenColor];

                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        //deselect
                        PaperColor = -1;
                        picCell.Image = paintCell(cell, picCell.Width, picCell.Height);
                    }
                }
            }

        }

        private void replaceColor(int degistirilecek, int yerineGelen)
        {
            int isMLT = cell.GetLength(1);
            for (int y = 0; y < isMLT; y++)
                for (int x = 0; x < 8; x++)
                {
                    if (cell[x, y] == degistirilecek) cell[x, y] = yerineGelen;
                }
        }

        private void picCell_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void picCell_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int zoomlevel = 16;
                int x = e.X / zoomlevel;
                int y = e.Y / zoomlevel;
                if (x > 7 || y > 7 || x < 0 || y < 0) return;
                if (y < 8)
                {
                    if (PenColor > -1)
                    {
                        //set a color
                        cell[x, y] = PenColor;
                        PaperColor = -1;
                        picCell.Image = paintCell(cell, picCell.Width, picCell.Height);
                        refreshCellPic(false);
                        chkValidCell.Checked = IsValidCell();
                    }
                }
            }
        }

        private bool IsValidCell()
        {
            if (ColorList.Count < 3)
            {
                if (ColorList.Count == 1) return true;
                if ((ColorList[0] > 7) && (ColorList[1] < 8) || (ColorList[1] > 7) && (ColorList[0] < 8))

                    return false;
                else
                    return true;

            }
            else
                return false;
        }
        private void btnRevertCell_Click(object sender, EventArgs e)
        {
            Array.Copy(cellUndo, cell, cellUndo.Length);
            refreshCellPic();
        }

        private void btnCellDone_Click(object sender, EventArgs e)
        {
            refreshCellPic(true);
            bool needswork = false;
            marks = checkMismatch(imgOriginal, out needswork);
            zoomSource(lastresize);
        }



        private bool CheckInvalid(int[,] cell)
        {

            int[] cols = new int[2];
            cols[0] = -1;
            cols[1] = -1;
            int mltOrScr = cell.GetLength(1);
            int bri = (cell[0, 0] > 7 ? 1 : 0); //set brightness check
            for (int rsy = 0; rsy < mltOrScr; rsy++)
            {
                for (int rsx = 0; rsx < 8; rsx++)
                {
                    int idx = cell[rsx, rsy];
                    if (idx >= 0)
                    {

                        //color exists
                        if ((bri == 1 && idx < 8) || (bri == 0 && idx > 7))
                        {
                            //brighness problem
                            return false;
                        }

                        if ((cols[0] == idx) || (cols[1] == idx))
                        {
                            //not a new color but it's a valid pre-stored color
                        }
                        else
                        {
                            // a new color
                            // check if any free ink or paper for 8x8block
                            if (cols[0] == -1)
                            {
                                cols[0] = idx;

                            }
                            else if (cols[1] == -1)
                            {
                                cols[1] = idx;

                            }
                            else
                            {
                                //out of free slots
                                //we have a problem. mark this block and move to next
                                return false;
                            }
                        }

                    }
                    else
                    {
                        //not indexed color
                        return false;
                    }


                }
            }
            //everything checks out
            return true;
        }

        private void picCell_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            bright = Convert.ToInt32(txtBright.Text);
            nobright = Convert.ToInt32(txtNormal.Text);
            setpalette();


        }

        private void button13_Click(object sender, EventArgs e)
        {
             for (int y=0;y<marks.GetLength(1);y++)
                 for (int x = 0; x < marks.GetLength(0); x++)
                 {
                     if ((marks[x, y] == -1))
                     {
                         selectCell(x, y);
                         return;
                     }
                 }
        }

        private void btnSavePNG_Click(object sender, EventArgs e)
        {
            SavePNG(imgOriginal);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            txtBright.Text = "251";
            txtNormal.Text = "200";
            nobright = 200;
            bright = 251;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            cell = flipBrighness(cell);
            
            refreshCellPic(false);
            chkValidCell.Checked = IsValidCell();
        }

        private int[,] flipBrighness(int[,] cells)
        {
            bool flip = false;
            for (int y = 0; y < cells.GetLength(1); y++)
                for (int x = 0; x < 8; x++)
                {
                    if (cells[x, y] > 7)
                    {
                        cells[x, y] -= 8;
                        flip = true;
                    }
                }

            if (flip) return cells;

            for (int y = 0; y < cells.GetLength(1); y++)
                for (int x = 0; x < 8; x++)
                {
                    if (cells[x, y] < 8)
                    {
                        cells[x, y] += 8;
                        flip = true;
                    }
                }
            return cells;
        }

        private void picDest_Move(object sender, EventArgs e)
        {
            picOriginal.Left = picDest.Left;
            picOriginal.Top = picDest.Top + picDest.Height + 8;
        }

        private void chkInvalids_EnabledChanged(object sender, EventArgs e)
        {
            chkQuantizeInvalids.Enabled = chkInvalids.Enabled;
        }

    }
}
