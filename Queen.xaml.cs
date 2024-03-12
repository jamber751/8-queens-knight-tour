namespace Algo7;

public partial class Queen : ContentPage
{
    public Queen()
    {
        InitializeComponent();
        updateBoard();
    }

    int[] a = new int[8];
    bool[] b = new bool[15];
    bool[] c = new bool[15];

    string bukvy = "ABCDEFGH";

    string[] solutions = new string[92];
    int solutionsCount = 0;

    String pathToQueen = "/Users/jrrrrr/Downloads/queen90x85.png";

    public bool TryQueen(int stroka)
    {
        for (int stolbec = 0; stolbec < 8; stolbec++)
        {
            if (a[stolbec] == -1 && !b[stroka - stolbec + 7] && !c[stroka + stolbec])
            {
                a[stolbec] = stroka;
                b[stroka - stolbec + 7] = true;
                c[stroka + stolbec] = true;

                if (stroka == 7) return true;
                if (TryQueen(stroka + 1)) return true;
                else
                {
                    a[stolbec] = -1;
                    b[stroka - stolbec + 7] = false;
                    c[stroka + stolbec] = false;
                }
            }

        }
        return false;
    }


    private void TryQueenAll(int stroka)
    {
        if (stroka == 8)
        {
            string[] text = new string[8];
            for (int i = 0; i < 8; i++) text[i] = bukvy[i] + (8 - a[i]).ToString();

            solutions[solutionsCount++] = String.Join(",", text);
            return;
        }

        for (int stolbec = 0; stolbec < 8; stolbec++)
        {
            if (a[stolbec] == -1 && !b[stroka - stolbec + 7] && !c[stroka + stolbec])
            {
                a[stolbec] = stroka;
                b[stroka - stolbec + 7] = true;
                c[stroka + stolbec] = true;

                TryQueenAll(stroka + 1);

                a[stolbec] = -1;
                b[stroka - stolbec + 7] = false;
                c[stroka + stolbec] = false;
            }
        }
    }

    public void resetBoard()
    {
        solutionsCount = 0;
        for (int i = 0; i < 15; i++)
        {
            if (i < 8) a[i] = -1;
            b[i] = false;
            c[i] = false;
        }
    }

    public void updateBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Button button = new Button();
                button.BackgroundColor = (i + j) % 2 == 0 ? Colors.White : Colors.Black;
                if (a[j] == i) button.ImageSource = pathToQueen;
                button.Padding = 0;
                chessBoard.Add(button, j, i);
            }
        }
    }

    void updateButton_Clicked(System.Object sender, System.EventArgs e)
    {
        resetBoard();
        TryQueen(0);
        updateBoard();
    }

    void updateButton2_Clicked(System.Object sender, System.EventArgs e)
    {
        resetBoard();
        updateBoard();
        TryQueenAll(0);
        resultList.ItemsSource = solutions;
    }

    void resultList_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            string[] solution = solutions[selectedIndex].Split(',');
            for (int i = 0; i < 8; i++)
            {
                char c = solution[i][1];
                a[i] = 8 - int.Parse(char.ToString(c));
            }
            updateBoard();
        }
    }

}
