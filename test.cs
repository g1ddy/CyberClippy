using Markdown.Avalonia;
using System;
using System.Reflection;

class Program {
    static void Main() {
        foreach (var p in typeof(MarkdownScrollViewer).GetProperties()) {
            if (p.Name.Contains("Html")) Console.WriteLine(p.Name);
        }
    }
}
