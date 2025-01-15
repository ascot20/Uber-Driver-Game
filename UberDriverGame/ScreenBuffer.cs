using System;

class ScreenBuffer
{
    const char spaceCharacter = ' ';
    const string separator = "\n";

    private int width;
    private int height;
    private char[,] buffer;
    private char[,] previousBuffer;
    
    public ScreenBuffer(int width, int height)
    {
        this.setScreenBufferDimension(width, height);
        this.clearBuffer();
    }

    public void setScreenBufferDimension(int screenWidth, int screenHeight)
    {
        this.width = screenWidth;
        this.height = screenHeight;
        this.buffer = new char[screenHeight, screenWidth];
        this.previousBuffer = new char[screenHeight, screenWidth];
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
        string[] lines = stringText.text.Split(separator);
        int stringHeight = lines.Length;

        for (int i = 0; i < stringHeight; i++)
        {
            BufferString s;
            s.xPos = stringText.xPos;
            s.yPos = stringText.yPos + i;
            s.text = lines[i];

            this.writeLine(s);
        }
    }

    //clear lines from 2D buffer array
    public void clearLines(BufferString stringText)
    {
        string[] lines = stringText.text.Split(separator);
        int stringHeight = lines.Length;

        for (int i = 0; i < stringHeight; i++)
        {
            BufferString s;
            s.xPos = stringText.xPos;
            s.yPos = stringText.yPos + i;
            s.text = lines[i];

            this.clearLine(s);
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
                    try
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(this.buffer[i, j]);
                        this.previousBuffer[i, j] = this.buffer[i, j];
                    }
                    catch (Exception e) 
                    {
                        throw new Exception(e.Message);
                    }
                    
                }
            }
        }
    }
}
