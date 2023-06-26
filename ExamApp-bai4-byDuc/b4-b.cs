using System;
using System.IO;
using System.Text.RegularExpressions;
using Novacode;

class AikenFormatChecker
{
    static void Main()
    {
        string filePath = "path/to/your/file.docx"; // Thay đổi đường dẫn tới tệp DOCX của bạn

        if (CheckAikenFormat(filePath))
        {
            Console.WriteLine("Success " + GetQuestionCount(filePath));
        }
    }

    static bool CheckAikenFormat(string filePath)
    {
        bool success = true;

        using (DocX document = DocX.Load(filePath))
        {
            int paragraphCount = document.Paragraphs.Count;

            for (int i = 0; i < paragraphCount; i++)
            {
                Paragraph paragraph = document.Paragraphs[i];
                string text = paragraph.Text.Trim();

                if (i == 0)
                {
                    // Kiểm tra tiêu đề câu hỏi
                    if (string.IsNullOrEmpty(text))
                    {
                        success = false;
                        Console.WriteLine("Error at paragraph " + (i + 1));
                    }
                }
                else if (i >= 1 && i <= paragraphCount - 2)
                {
                    // Kiểm tra danh sách các phát biểu
                    if (!Regex.IsMatch(text, @"^[A-Z]\.\s\S+"))
                    {
                        success = false;
                        Console.WriteLine("Error at paragraph " + (i + 1));
                    }
                }
                else if (i == paragraphCount - 1)
                {
                    // Kiểm tra dòng ANSWER
                    if (!Regex.IsMatch(text, @"^ANSWER:\s[A-Z]$"))
                    {
                        success = false;
                        Console.WriteLine("Error at paragraph " + (i + 1));
                    }
                }
            }
        }

        return success;
    }

    static int GetQuestionCount(string filePath)
    {
        using (DocX document = DocX.Load(filePath))
        {
            int paragraphCount = document.Paragraphs.Count;
            return (paragraphCount - 2) / 3; // Số câu hỏi = (tổng số đoạn văn - 2) / 3
        }
    }
}
