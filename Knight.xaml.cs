namespace Algo7;

public partial class Knight : ContentPage
{

    public Knight()
    {
        InitializeComponent();
        createBoard();
    }

    String pathToKnight = "/Users/jrrrrr/Downloads/knight90x90.png";


    int[,] visited = new int[8, 8];
    static int[] hodStroka = { 2, 1, -1, -2, -2, -1, 1, 2 };
    static int[] hodStobec = { 1, 2, 2, 1, -1, -2, -2, -1 };


    int currentMove = -1;
    Button[,] board = new Button[8, 8];
    bool selection = true;
    (int, int) start;


    public bool KnightSearch(int stroka, int stolbec, int hod)
    {
        if (hod == 64) return true;

        //List<(int, int)> hodi = new List<(int, int)>();

        for (int i = 0; i < hodStobec.Length; i++)
        {
            int newStroka = stroka + hodStroka[i];
            int newStolbec = stolbec + hodStobec[i];
            if (HodVozmozhen(newStroka, newStolbec))
            {
                hod++;
                visited[newStroka, newStolbec] = hod;
                if (KnightSearch(newStroka, newStolbec, hod))
                    return true;
                else
                {
                    visited[newStroka, newStolbec] = 0;
                    hod--;
                }
            }
        }
        return false;
    }


    public int Kolichestvo(int stroka, int stolbec)
    {
        int count = 0;
        for (int i = 0; i < hodStobec.Length; i++)
        {
            if (HodVozmozhen(stroka + hodStroka[i], stolbec + hodStobec[i]))
            {
                count++;
            }
        }
        return count;
    }


    public bool HodVozmozhen(int stroka, int stolbec)
    {
        return stroka >= 0 && stroka < 8 && stolbec >= 0 && stolbec < 8 && visited[stroka, stolbec] == 0;
    }


    public void VyborNachala(int i, int j)
    {
        resetBoard();
        if (selection)
        {
            board[i, j].ImageSource = pathToKnight;
            start = (i, j);
            searchButton.IsEnabled = true;
            selection = false;
        }
    }


    void searchButton_Clicked(System.Object sender, System.EventArgs e)
    {
        searchButton.IsEnabled = false;
        resetBoard();
        visited[start.Item1, start.Item2] = 1;
        KnightSearch(start.Item1, start.Item2, 1);
        currentMove = 1;
        updateBoard();
        nextButton.IsEnabled = true;
    }


    void resetButton_Clicked(System.Object sender, System.EventArgs e)
    {
        currentMove = -1;
        searchButton.IsEnabled = false;
        nextButton.IsEnabled = false;
        prevButton.IsEnabled = false;
        selection = true;
        resetBoard();
        updateBoard();
    }


    void nextButton_Clicked(System.Object sender, System.EventArgs e)
    {
        currentMove++;
        if (currentMove == 64) nextButton.IsEnabled = false;
        else if (currentMove == 2) prevButton.IsEnabled = true;
        updateBoard();
    }


    void prevButton_Clicked(System.Object sender, System.EventArgs e)
    {
        currentMove--;
        if (currentMove == 1) prevButton.IsEnabled = false;
        else if (currentMove == 63) nextButton.IsEnabled = true;
        updateBoard();
    }


    public void createBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                visited[i, j] = 0;
                Button button = new Button();
                button.BackgroundColor = (i + j) % 2 == 0 ? Colors.White : Colors.Black;
                button.Padding = 0;

                int currentI = i;
                int currentJ = j;
                button.Clicked += (s, e) => VyborNachala(currentI, currentJ);

                board[i, j] = button;
                chessBoard.Add(button, j, i);
            }
        }
    }


    public void resetBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                visited[i, j] = 0;
                board[i, j].BackgroundColor = (i + j) % 2 == 0 ? Colors.White : Colors.Black;
                board[i, j].Padding = 0;
                board[i, j].Text = String.Empty;
            }
        }
    }


    public void updateBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (visited[i, j] == currentMove) board[i, j].ImageSource = pathToKnight;
                else board[i, j].ImageSource = "";

                if (visited[i, j] < currentMove && currentMove > 0)
                {
                    board[i, j].Text = visited[i, j].ToString();
                    board[i, j].FontSize = 25;
                    if ((i + j) % 2 == 0)
                    {
                        board[i, j].TextColor = Color.FromArgb("144e00");
                        board[i, j].BackgroundColor = Color.FromArgb("70aa5c");
                    }
                    else
                    {
                        board[i, j].TextColor = Color.FromArgb("70aa5c");
                        board[i, j].BackgroundColor = Color.FromArgb("144e00");
                    }
                }
                else
                {
                    board[i, j].Text = String.Empty;
                    board[i, j].BackgroundColor = (i + j) % 2 == 0 ? Colors.White : Colors.Black;
                }
            }
        }
    }

}