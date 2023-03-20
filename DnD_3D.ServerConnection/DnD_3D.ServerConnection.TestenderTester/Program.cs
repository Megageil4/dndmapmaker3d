// See https://aka.ms/new-console-template for more information
using DnD_3D.ServerConnection.Default;

IDnDConnection albrecht = new WebsocketDnDConnection("ws://127.0.0.1:7890/Echo");

albrecht.MapExistsAntwort += (sender, e) =>
{
    Console.WriteLine(e.ergebnis);
};

albrecht.MapExists();
Console.ReadKey();