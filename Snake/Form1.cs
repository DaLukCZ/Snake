using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        int cols = 40;
        int rows = 40;
        int dx = 0;
        int dy = 0;
        int front = 0;
        int back = 0;
        int score = 0;
        int seconds = 0;
        bool right = true;
        bool left = true;
        bool up = true;
        bool down = true;
        Pixel[] snake = new Pixel[1600];
        List<int> free = new List<int>();
        bool[,] visit;
        Random random = new Random();
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
            init();
            startTimer();
        }

        private void startTimer()
        {
            timer1.Interval = 100;
            timer1.Tick += move;
            timer1.Start();
        }

        private void move(object sender, EventArgs e)
        {
            int x = snake[front].Location.X;
            int y = snake[front].Location.Y;
            if (dx == 0 && dy == 0)
            {
                return;
            }
            if (gameOver(x + dx, y + dy))
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                return;
            }
            if (collisionFood(x + dx, y + dy))
            {
                score++;
                labelScore.Text = "Score: " + score;
                if (hits((y + dy) / 20, (x + dx) / 20))
                {
                    return;
                }
                Pixel head = new Pixel(x + dx, y + dy);
                front = (front - 1 + 1600) % 1600;
                snake[front] = head;
                visit[head.Location.Y / 20, head.Location.X / 20] = true;
                Controls.Add(head);
                randomFood();
            }
            else
            {
                if (hits((y + dy) / 20, (x + dx) / 20))
                {
                    return;
                }
                visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] = false;
                front = (front - 1 + 1600) % 1600;
                snake[front] = snake[back];
                snake[front].Location = new Point(x + dx, y + dy);
                back = (back - 1 + 1600) % 1600;
                visit[(y + dy) / 20, (x + dx) / 20] = true;
            }
        }

        private void randomFood()
        {
            free.Clear();
            if (!win())
            {
                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < cols; y++)
                    {
                        if (!visit[x, y])
                        {
                            free.Add(x * cols + y);
                        }
                    }
                }
                food.Location = new Point((random.Next() % cols) * 20, (random.Next() % rows) * 20);
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("vyhrál jsi lol");
            }
        }

        private bool win()
        {
            if(score == 1600)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool hits(int x, int y)
        {
            if (visit[x, y])
            {
                timer1.Stop();
                MessageBox.Show("Snake hit his body");
                return true;
            }
            return false;
        }

        private bool collisionFood(int x, int y)
        {
            return x == food.Location.X && y == food.Location.Y; 
        }

        private bool gameOver(int x, int y)
        {
            return x < 0 || y < 0 || x > 799 || y > 799;
        }

        private void init()
        {
            visit = new bool[rows, cols];
            Pixel head = new Pixel((random.Next() % cols) * 20, (random.Next() % rows) * 20);
            food.Location = new Point((random.Next() % cols) * 20, (random.Next() % rows) * 20);

            for(int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    visit[x, y] = false;
                    free.Add(x * cols + y);
                }
                visit[head.Location.Y / 20, head.Location.X / 20] = true;
                free.Remove(head.Location.Y / 20 * cols + head.Location.X / 20);
                Controls.Add(head);
                snake[front] = head;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer2.Start();
            dx = dy = 0;
            switch (e.KeyCode)
            {

                case Keys.Right:
                    if (right)
                    {
                        left = false;
                        up = true;
                        down = true;
                        dx = 20;
                    }
                    else
                    {
                        dx = -20;
                    }
                    break;
                case Keys.Left:
                    if (left)
                    {
                        right = false;
                        up = true;
                        down = true;
                        dx = -20;
                    }
                    else
                    {
                        dx = 20;
                    }
                    break;
                case Keys.Up:
                    if (up)
                    {
                        down = false;
                        right = true;
                        left = true;
                        dy = -20;
                    }
                    else
                    {
                        dy = 20;
                    }
                    break;
                case Keys.Down:
                    if (down)
                    {
                        up = false;
                        right = true;
                        left = true;
                        dy = 20;
                    }
                    else
                    {
                        dy = -20;
                    }
                    break;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            labelTime.Text = "Time: " + seconds++ + " s";
        }
    }
}
