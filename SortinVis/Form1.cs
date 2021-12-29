using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortinVis
{
    public partial class Form1 : Form
    {
        int[] array;
        int N;
        int mil = 1;

        PBox[] boxes;
        Point[] coordinates;
        Color[] colors;

        Button shuffleButton;
        Button bubbleButton;
        Button insertButton;
        Button quickButton;
        Button shakerButton;
        Button mergeButton;
        public Form1()
        {
            InitializeComponent();
#region Init
            N = 256;
            array = new int[N];

            colors = GetSpectrum();
            int shift = Convert.ToInt32(Math.Floor(255.0 / N));
            coordinates = new Point[N];
            int start = 0;
            for(int i = 0; i<N; i++)
            {
                array[i] = i;
                coordinates[i] = new Point(start, 10);
                start += 4;
            }

            boxes = new PBox[N];

            for (int i = 0; i < N; i++)
            {
                boxes[i] = new PBox()
                {
                    Location = coordinates[i],
                    //Height = 500 - i*2+1,
                    Height = 500,
                    Width = 4,
                    BackColor = colors[i],
                    Value = array[i]
                };
                Controls.Add(boxes[i]);
            }

            #endregion
            

            #region elements
            shuffleButton = new Button()
            {
                Location = new Point(1200, 10),
                AutoSize = true,
                Text = "Shuffle",
                ForeColor = Color.White
            };
            shuffleButton.Click += ShuffleButton_Click;
            Controls.Add(shuffleButton);

            bubbleButton = new Button()
            {
                Location = new Point(1200, 50),
                AutoSize = true,
                Text = "Bubble sort",
                ForeColor = Color.White
            };
            bubbleButton.Click += BubbleButton_Click;
            Controls.Add(bubbleButton);

            insertButton = new Button()
            {
                Location = new Point(1200, 90),
                AutoSize = true,
                Text = "Insertion sort",
                ForeColor = Color.White
            };
            insertButton.Click += InsertButton_Click;
            Controls.Add(insertButton);

            quickButton = new Button()
            {
                Location = new Point(1200, 130),
                AutoSize = true,
                Text = "Quick sort",
                ForeColor = Color.White
            };
            quickButton.Click += QuickButton_Click;
            Controls.Add(quickButton);

            shakerButton = new Button()
            {
                Location = new Point(1200, 170),
                AutoSize = true,
                Text = "Shaker sort",
                ForeColor = Color.White
            };
            shakerButton.Click += ShakerButton_Click;
            Controls.Add(shakerButton);


            mergeButton = new Button()
            {
                Location = new Point(1200, 210),
                AutoSize = true,
                Text = "Merge sort",
                ForeColor = Color.White
            };
            mergeButton.Click += MergeButton_Click;
            Controls.Add(mergeButton);

            Width = 1300;
            Height = 600;
            BackColor = Color.FromArgb(30, 30, 30);
            #endregion
        }

        private void MergeButton_Click(object sender, EventArgs e)
        {
            mil = 5;
            MergeSort(0, N - 1);
            UpdateScreen();
            Update();
        }

        private void ShakerButton_Click(object sender, EventArgs e)
        {
            mil = 0;
            ShakerSort();
            UpdateScreen();
        }

        private void QuickButton_Click(object sender, EventArgs e)
        {
            mil = 10;
            QuickSort();
            UpdateScreen();
        }

        public void UpdateScreen()
        {
            MatchArray();
            for (int i = 0; i < N; i++)
            {
                boxes[i].Location = coordinates[i];
            }
            System.Threading.Thread.Sleep(mil);
            Update();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            mil = 0;
            InsertionSort();
            UpdateScreen();
        }

        private void BubbleButton_Click(object sender, EventArgs e)
        {
            mil = 0;
            BubbleSort();
            UpdateScreen();
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            mil = 100;
            ShuffleArray();
            UpdateScreen();
        }

        public void ShuffleArray()
        {
            Random random = new Random();
            int position = 0;
            for(int i = 0; i<N; i++)
            {
                position = random.Next(i, N);
                SwapValues(i, position);
            }
        }

        public void ShakerSort()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        SwapValues(j, j + 1);
                        UpdateScreen();
                    }
                }
                for(int j = N-i-1; j>0; j--)
                {
                    if(array[j]<array[j-1])
                    {
                        SwapValues(j, j - 1);
                        UpdateScreen();
                    }
                }
            }
        }
        public void BubbleSort()
        {
            for(int i = 0; i<N; i++)
            {
                for(int j = 0; j<N-i-1; j++)
                {
                    if(array[j]>array[j+1])
                    {
                        SwapValues(j, j+1);
                        UpdateScreen();
                    }
                }
            }
        }

        public void InsertionSort()
        {
            for(int i = 1; i<N; i++)
            {
                for(int j = i; j>0; j--)
                {
                    if(array[j-1]>array[j])
                    {
                        SwapValues(j, j - 1);
                        UpdateScreen();
                    }
                }
            }
            
        }
        public int Partition(int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;
            for (int i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    SwapValues(pivot, i);
                    UpdateScreen();
                }
            }

            pivot++;
            SwapValues(pivot, maxIndex);
            return pivot;
        }

        public void QuickSort(int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
                return;
            int pivotIndex = Partition(minIndex, maxIndex);
            QuickSort(minIndex, pivotIndex - 1);
            QuickSort(pivotIndex + 1, maxIndex);
            return;
        }

        public void QuickSort()
        {
            QuickSort(0, N-1);
            UpdateScreen();
        }

        public void MergeSort(int left, int right)
        {
            int middle;
            if(left < right)
            {
                middle = (left + right) / 2;
                MergeSort(left, middle);
                MergeSort(middle+1, right);
                Merge(left, middle, right);
                UpdateScreen();
            }
        }

        public void Merge(int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(array, left, leftArray, 0, middle - left + 1);
            Array.Copy(array, middle + 1, rightArray, 0, right - middle);


            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    array[k] = rightArray[j];
                    j++;
                    UpdateScreen();
                }
                else if (j == rightArray.Length)
                {
                    array[k] = leftArray[i];
                    i++;
                    UpdateScreen();
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                    UpdateScreen();
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                    UpdateScreen();
                }
            }
        }
        public void SwapValues(int x, int y)
        {
            if(x == -1 || y == -1) return;
            int tmp = array[x];
            array[x] = array[y];
            array[y] = tmp;
            return;
        }

        public void SwapValues(int x, int y, bool pbox)
        {
            if (x == -1 || y == -1) return;
            PBox tmp = boxes[x];
            boxes[x] = boxes[y];
            boxes[y] = tmp;
        }

        public void MatchArray()
        {
            int pos = 0;
            for(int i = 0; i<N; i++)
            {
                pos = Array.IndexOf(array, boxes[i].Value);
                SwapValues(i, pos, true);
            }
        }

        public Color[] GetSpectrum()
        {
            int R, G, B;
            R = 255;
            G = 0;
            B = 0;
            Color[] result = new Color[256];
            result[0] = Color.FromArgb(R, G, B);
            for(int i = 1; i<64; i++)
            {
                G += 4;
                result[i] = Normalize(R, G, B);
            }

            for (int i = 64; i < 128; i++)
            {
                R -= 4;
                result[i] = Normalize(R, G, B);
            }

            for (int i = 128; i < 192; i++)
            {
                B += 4;
                result[i] = Normalize(R, G, B);
            }

            for (int i = 192; i < 256; i++)
            {
                G -= 4;
                result[i] = Normalize(R, G, B);
            }

            return result;
        }

        private Color Normalize(int R, int G, int B)
        {
            int newR = R > 255 ? 255 : R < 0 ? 0 : R;
            int newG = G > 255 ? 255 : G < 0 ? 0 : G;
            int newB = B > 255 ? 255 : B < 0 ? 0 : B;
            return Color.FromArgb(newR, newG, newB);
        }
    }

    public class PBox : PictureBox
    {
        public int Value { get; set; }
    }
}
