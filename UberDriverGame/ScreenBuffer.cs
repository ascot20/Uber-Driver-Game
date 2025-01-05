using System;

class ScreenBuffer
{
    //constants
    const char spaceCharacter = ' ';

    //fields
    private int width;
    private int height;
    private char[,] buffer;
    private char[,] previousBuffer;
    

    public ScreenBuffer(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.buffer = new char[height, width];
        this.previousBuffer = new char[height, width];
        this.clearBuffer();
    }

    //initialize 2D buffer array with space character
    public void clearBuffer()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                this.buffer[i, j] = spaceCharacter;
                this.previousBuffer[i, j] = spaceCharacter;
            }
        }
    }

    //write a character to 2D buffer array
    public void writeChar(BufferChar c)
    {
        if ((c.xPos >= 0 && c.xPos < this.width) && (c.yPos >= 0 && c.yPos < this.height))
        {
            this.buffer[c.yPos, c.xPos] = c.character;
        }
    }

    //clear a character from 2D buffer array
    public void clearChar(BufferChar c)
    {
        if ((c.xPos >= 0 && c.xPos < this.width) && (c.yPos >= 0 && c.yPos < this.height))
        {
            this.buffer[c.yPos, c.xPos] = ' ';
        }
    }

    //write a line to the 2D buffer array
    public void writeLine(BufferString stringText)
    {
        int stringLength = stringText.text.Length;

        for (int i = 0; i < stringLength; i++)
        {
            BufferChar c;
            c.xPos = stringText.xPos + i;
            c.yPos = stringText.yPos;
            c.character = stringText.text[i];

            writeChar(c);
        }
    }

    //clear a line from 2D buffer array
    public void clearLine(BufferString stringText)
    {
        int stringLength = stringText.text.Length;

        for (int i = 0; i < stringLength; i++)
        {
            BufferChar c;
            c.xPos = stringText.xPos + i;
            c.yPos = stringText.yPos;
            c.character = spaceCharacter;

            this.clearChar(c);
        }
    }

    //write lines to 2D buffer array
    public void writeLines(BufferString stringText)
    {
        string[] lines = stringText.text.Split("\n");
        int stringHeight = lines.Length;

        for (int i = 0; i < stringHeight; i++)
        {
            BufferString c;
            c.xPos = stringText.xPos;
            c.yPos = stringText.yPos + i;
            c.text = lines[i];

            this.writeLine(c);
        }
    }

    //clear lines from 2D buffer array
    public void clearLines(BufferString stringText)
    {
        string[] lines = stringText.text.Split("\n");
        int stringHeight = lines.Length;

        for (int i = 0; i < stringHeight; i++)
        {
            BufferString c;
            c.xPos = stringText.xPos;
            c.yPos = stringText.yPos + i;
            c.text = lines[i];

            this.clearLine(c);
        }
    }

    //render the 2D buffer array to console
    public void renderToConsole()
    {
        for(int i = 0; i < this.height; i++)
        {
            for(int j = 0;j < this.width; j++)
            {
                if (this.buffer[i,j] != this.previousBuffer[i, j])
                {
                    Console.SetCursorPosition(j,i);
                    Console.Write(this.buffer[i,j]);
                    this.previousBuffer[i,j] = this.buffer[i,j];
                }
            }
        }
    }
}

struct BufferChar
{
    public int xPos;
    public int yPos;
    public char character;
}

struct BufferString
{
    public int xPos;
    public int yPos;
    public string text;
}
