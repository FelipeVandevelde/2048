using System;
using System.IO;

namespace _2048
{
    internal class Program
    {
        static int[,] mapa = new int[4, 4];

        static Random aleatorio= new Random();

        static string scoreAnotado;

        static int score, moveu = 1;

        static void Main(string[] args)
        {
            string teclaApertada;
            HighScore(1);

            do
            {
                if (moveu == 1)
                {
                    GerarNovoBloco();
                    moveu = 0;
                }

                do
                {
                    Console.Clear();
                    if (Validacao() == 0)
                    { 
                        Visual();
                    }
                    else
                    {
                        EndGame();
                    }
                    teclaApertada = Tecla();
                } while (teclaApertada != "LeftArrow" && teclaApertada != "A" && teclaApertada != "a" && teclaApertada != "RightArrow" && teclaApertada != "D" && teclaApertada != "d"  && teclaApertada != "UpArrow" && teclaApertada != "W" && teclaApertada != "w" && teclaApertada != "DownArrow" && teclaApertada != "S" && teclaApertada != "s" && teclaApertada != "Escape" && teclaApertada != "Enter");

                if (teclaApertada != "Escape")
                {
                    Acao(teclaApertada);
                }else 
                if (teclaApertada == "Enter")
                {
                    mapa = new int[4, 4];
                    moveu = 1;
                }

                Console.Clear();
            } while (teclaApertada != "Escape");
        }

        static string Tecla()
        {
            var tecla = Console.ReadKey();
            return Convert.ToString(tecla.Key);
        }

        static void Visual()
        {
            Console.Write($"\t\t\tPara sair aperte 'esc'\n\n\n\n\t\t\t     2048\n\n\t\t   Score: {score}");
            Console.Write($"\t   HighScore: {scoreAnotado}\n\n");
            Console.WriteLine("\t\t╔═══════╦═══════╦═══════╦═══════╗");

            for (int linha = 0; linha < 4; linha++)
            {
                Console.Write("\t\t║ ");
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    string valor = "";
                    if (mapa[linha, coluna] > 0)
                    {
                        string vazio;
                        switch (mapa[linha, coluna].ToString().Length)
                        {
                            case 4:
                                vazio = "";
                                break;
                            case 3:
                                vazio = " ";
                                break;
                            case 2:
                                vazio = "  ";
                                break;
                            default:
                                vazio = "   ";
                                break;
                        }

                        valor = string.Concat(vazio, mapa[linha, coluna]);
                    }

                    CorBloco(mapa[linha, coluna]);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(valor);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("\t║ ");

                }

                if(linha+1 < 4)
                {
                    Console.Write("\n\t\t╠═══════╬═══════╬═══════╬═══════╣");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\t\t╚═══════╩═══════╩═══════╩═══════╝");

            void CorBloco(int valor)
            {
                switch (valor)
                {
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;

                    case 4:
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;

                    case 8:
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        break;

                    case 16:
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;

                    case 32:
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        break;

                    case 64:
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        break;

                    case 128:
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        break;

                    case 256:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        break;

                    case 512:
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        break;

                    case 1024:
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;

                    case 2048:
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;

                    default:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                }
            }
        }

        static void GerarNovoBloco()
        {
            int linha, coluna, bloco;

            do
            {
                linha = aleatorio.Next(0, 4);
                coluna = aleatorio.Next(0, 4);
            } while (mapa[linha, coluna] > 0);

            bloco = aleatorio.Next(2);

            if (bloco == 0)
            {
                mapa[linha, coluna] = 2;
            }
            else
            {
                mapa[linha, coluna] = 4;
            }

        }

        static void Acao(string tecla)
        {
            switch (tecla)
            {
                case "Enter":
                    mapa = new int[4, 4];
                    moveu = 1;
                    score = 0;
                    HighScore(1);
                    break;

                case "LeftArrow":
                case "A":
                case "a":
                    ArrastaEsquerda();
                    SomaEsquerda();
                    break;

                case "RightArrow":
                case "D":
                case "d":
                    ArrastaDireita();
                    SomaDireita();
                    break;

                case "UpArrow":
                case "W":
                case "w":
                    ArrastaCima();
                    SomaCima();
                    break;

                case "DownArrow":
                case "S":
                case "s":
                    ArrastaBaixo();
                    SomaBaixo();
                    break;
            };

            void SomaEsquerda()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    for (int coluna = 1; coluna < 4; coluna++)
                    {
                        if (mapa[linha, coluna] == mapa[linha, coluna - 1])
                        {
                            mapa[linha, coluna - 1] = mapa[linha, coluna] * 2;
                            score += mapa[linha, coluna - 1];
                            mapa[linha, coluna] = 0;
                            ArrastaEsquerda();
                        }
                    }
                }
            }
            void ArrastaEsquerda()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    int pularColunas = 0;
                    for (int coluna = 0; coluna < 4; coluna++)
                    {
                        if (mapa[linha, coluna] == 0)
                        {
                            pularColunas++;
                        }
                        else
                        {
                            mapa[linha, coluna - pularColunas] = mapa[linha, coluna];

                            if (pularColunas != 0)
                            {
                                mapa[linha, coluna] = 0;
                                pularColunas = 0;
                                coluna = 0;
                                moveu = 1;
                            }

                        }
                    }
                }
            }

            void SomaDireita()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    for (int coluna = 2; coluna >= 0; coluna--)
                    {
                        if (mapa[linha, coluna] == mapa[linha, coluna + 1])
                        {
                            mapa[linha, coluna + 1] = mapa[linha, coluna] * 2;
                            score += mapa[linha, coluna + 1];
                            mapa[linha, coluna] = 0;
                            ArrastaDireita();
                        }
                    }
                }
            }
            void ArrastaDireita()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    int pularColunas = 0;
                    for (int coluna = 3; coluna >= 0; coluna--)
                    {
                        if (mapa[linha, coluna] == 0)
                        {
                            pularColunas++;
                        }
                        else
                        {
                            mapa[linha, coluna + pularColunas] = mapa[linha, coluna];

                            if (pularColunas != 0)
                            {
                                mapa[linha, coluna] = 0;
                                pularColunas = 0;
                                coluna = 3;
                                moveu = 1;
                            }

                        }
                    }
                }
            }

            void SomaCima()
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    for (int linha = 1; linha < 4; linha++)
                    {
                        if (mapa[linha, coluna] == mapa[linha - 1, coluna])
                        {
                            mapa[linha - 1, coluna] = mapa[linha, coluna] * 2;
                            score += mapa[linha - 1, coluna];
                            mapa[linha, coluna] = 0;
                            ArrastaCima();
                        }
                    }
                }
            }
            void ArrastaCima()
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    int pularLinha = 0;
                    for (int linha = 0; linha < 4; linha++)
                    {
                        if (mapa[linha, coluna] == 0)
                        {
                            pularLinha++;
                        }
                        else
                        {
                            mapa[linha - pularLinha, coluna] = mapa[linha, coluna];

                            if (pularLinha != 0)
                            {
                                mapa[linha, coluna] = 0;
                                pularLinha = 0;
                                linha = 0;
                                moveu = 1;
                            }

                        }
                    }
                }
            }

            void SomaBaixo()
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    for (int linha = 2; linha >= 0; linha--)
                    {
                        if (mapa[linha, coluna] == mapa[linha + 1, coluna])
                        {
                            mapa[linha + 1, coluna] = mapa[linha, coluna] * 2;
                            score += mapa[linha + 1, coluna];
                            mapa[linha, coluna] = 0;
                            ArrastaBaixo();
                        }
                    }
                }
            }
            void ArrastaBaixo()
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    int pularLinha = 0;
                    for (int linha = 3; linha >= 0; linha--)
                    {
                        if (mapa[linha, coluna] == 0)
                        {
                            pularLinha++;
                        }
                        else
                        {
                            mapa[linha + pularLinha, coluna] = mapa[linha, coluna];

                            if (pularLinha != 0)
                            {
                                mapa[linha, coluna] = 0;
                                pularLinha = 0;
                                linha = 3;
                                moveu = 1;
                            }

                        }
                    }
                }
            }
        }

        static void HighScore(int acao)
        {
            string texto = @"<LOCAL_FILE>"; //Encontre o arquivo "highScore.txt" e com o botão direito do mouse selecione "Copiar como caminho" e cole o caminho do arquivo

            switch (acao)
            {
                case 0:
                    using (StreamWriter sw = new StreamWriter(texto))
                    {
                        sw.WriteLine(score);
                    }
                    break;

                case 1:
                    using (StreamReader sr = new StreamReader(texto))
                    {
                        scoreAnotado = sr.ReadLine();
                    }
                    break;
            }

        }

        static int Validacao()
        {
            int endGame = 1;

            VerificarVazio();
            if(endGame == 1)
            {
                VerificarEsquerda();
            }
            if (endGame == 1)
            {
                VerificarDireita();
            }
            if (endGame == 1)
            {
                VerificarCima();
            }
            if (endGame == 1)
            {
                VerificarBaixo();
            }

            return endGame;

            void VerificarVazio()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    for (int coluna = 0; coluna < 4; coluna++)
                    {
                        if (mapa[linha, coluna] == 0)
                        {
                            endGame = 0;
                        }
                    }
                }
            }
            void VerificarEsquerda()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    for (int coluna = 1; coluna < 4; coluna++)
                    {
                        if (mapa[linha, coluna] == mapa[linha, coluna - 1])
                        {
                            endGame = 0;
                        }
                    }
                }
            }
            void VerificarDireita()
            {
                for (int linha = 0; linha < 4; linha++)
                {
                    for (int coluna = 2; coluna >= 0; coluna--)
                    {
                        if (mapa[linha, coluna] == mapa[linha, coluna + 1])
                        {
                            endGame = 0;
                        }
                    }
                }
            }
            void VerificarCima()
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    for (int linha = 1; linha < 4; linha++)
                    {
                        if (mapa[linha, coluna] == mapa[linha - 1, coluna])
                        {
                            endGame = 0;
                        }
                    }
                }
            }
            void VerificarBaixo()
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    for (int linha = 2; linha >= 0; linha--)
                    {
                        if (mapa[linha, coluna] == mapa[linha + 1, coluna])
                        {
                            endGame = 0;
                        }
                    }
                }
            }
        }

        static void EndGame()
        {
            if (score >= int.Parse(scoreAnotado))
            {
                HighScore(0);
            }

            Console.Write("\t\t\t  ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("FIM DE JOGO\n\n\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\t\t\tPara reiniciar aperte 'enter'\n");
            Visual();

        }

    }
}