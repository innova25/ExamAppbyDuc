using System;
using System.IO;
using System.Text.RegularExpressions;

class AikenFormatChecker
{
    static void Main()
    {
        string filePath = "path/to/your/file.txt"; // Thay đổi đường dẫn tới tệp văn bản của bạn

        if (CheckAikenFormat(filePath))
        {
            Console.WriteLine("Success " + GetQuestionCount(filePath));
        }
    }

    static bool CheckAikenFormat(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        int lineCount = lines.Length;
        bool success = true;

        for (int i = 0; i < lineCount; i++)
        {
            string line = lines[i].Trim();

            if (i == 0)
            {
                // Kiểm tra tiêu đề câu hỏi
                if (string.IsNullOrEmpty(line))
                {
                    success = false;
                    Console.WriteLine("Error at line " + (i + 1));
                }
            }
            else if (i >= 1 && i <= lineCount - 2)
            {
                // Kiểm tra danh sách các phát biểu
                if (!Regex.IsMatch(line, @"^[A-Z]\.\s\S+"))
                {
                    success = false;
                    Console.WriteLine("Error at line " + (i + 1));
                }
            }
            else if (i == lineCount - 1)
            {
                // Kiểm tra dòng ANSWER
                if (!Regex.IsMatch(line, @"^ANSWER:\s[A-Z]$"))
                {
                    success = false;
                    Console.WriteLine("Error at line " + (i + 1));
                }
            }
        }

        return success;
    }

    static int GetQuestionCount(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        return (lines.Length - 2) / 3; // Số câu hỏi = (tổng số dòng - 2) / 3
    }
}