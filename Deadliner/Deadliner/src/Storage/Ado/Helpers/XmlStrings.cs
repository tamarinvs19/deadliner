using System;
using System.Reflection;
using System.Xml;

namespace Deadliner.Storage.Ado.Helpers;

public class XmlStrings
{
    private static readonly XmlDocument xmlDocument;

    static XmlStrings()
    {
        xmlDocument = LoadXml();
    }

    private static XmlDocument LoadXml()
    {
        Assembly resourceAssembly = Assembly.GetExecutingAssembly();
        var xmlDoc = new XmlDocument();
        var stream = resourceAssembly.GetManifestResourceStream("Deadliner.src.Storage.Ado.Helpers.Queries.xml");
        xmlDoc.Load(stream);
        return xmlDoc;
    }

    public static string GetString(string commandName)
    {
        return GetString(string.Empty, commandName);
    }

    public static string GetString(string tableName, string commandName)
    {
        if (tableName == null)
            throw new ArgumentNullException("tableName");
        if (commandName == null)
            throw new ArgumentNullException("commandName");
        if (tableName == string.Empty)
            tableName = "CUSTOM_COMMANDS";

        return LoadCommand(tableName, commandName);
    }

    private static string LoadCommand(string tableName, string commandName)
    {
        XmlNode? xNode = GetXmlNode(tableName, commandName);
        
        string commandText = xNode.InnerText.Trim(' ', '\r', '\n', '\t');

        return commandText;
    }

    private static XmlNode? GetXmlNode(string tableName, string commandName)
    {
        return xmlDocument?.DocumentElement?.SelectSingleNode(
            $"Table[@Name=\"{tableName}\"]/Command[@Name=\"{commandName}\"]"
        );
    }
}