using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Tibia.Objects;

namespace Tibia.Util
{
    public class ClientChooserBase
    {
        public const string NewClientDefaultText = "New default client...";
        public const string NewClientCustomText = "New client (choose location)...";

        public static Client ChooseClient(ClientChooserOptions options, object selectedItem, LoginServer ls)
        {
            Client client = null;
            if (selectedItem.GetType() == typeof(string))
            {
                switch ((string)selectedItem)
                {
                    case NewClientDefaultText:
                        client = Client.OpenMC();
                        break;
                    case NewClientCustomText:
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter =
                           "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                        dialog.Title = "Select a Tibia client executable";
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(dialog.FileName);
                            if (fvi.ProductName.Equals("Tibia Player"))
                            {
                                client = Client.Open(dialog.FileName, options.Arguments);
                                if (options.SaveClientPath == true)
                                {
                                    ClientChooserBase.SaveClientPath(
                                        options.SavedClientPathsLocation,
                                        dialog.FileName,
                                        fvi.FileVersion);
                                }

                            }
                            else
                            {
                                client = null;
                            }
                        }
                        else
                        {
                            client = null;
                        }
                        break;
                }
            }
            else if (selectedItem.GetType() == typeof(ClientPathInfo))
            {
                string clientPath = ((ClientPathInfo)selectedItem).Path;
                Version.Set(FileVersionInfo.GetVersionInfo(clientPath).FileVersion);
                client = Client.OpenMC(clientPath, options.Arguments);
            }
            else
            {
                client = (Client)selectedItem;
            }

            // Set addresses if using an older client
            if (client != null && client.VersionNumber < Version.CurrentVersion)
                Version.Set(client.Version);

            // Set OT server
            if (client != null && options.UseOT)
            {
                client.Login.SetOT(ls);
            }

            return client;
        }

        public static List<ClientPathInfo> GetClientPaths(string location)
        {
            List<ClientPathInfo> clientPaths = new List<ClientPathInfo>();
            if (File.Exists(location))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(location);
                    string path;
                    string version;
                    foreach (XmlElement clientPath in document["clientPaths"])
                    {
                        path = clientPath.GetAttribute("location");
                        version = clientPath.GetAttribute("version");
                        clientPaths.Add(new ClientPathInfo(path, version));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return clientPaths;
        }
        public static void SaveClientPath(string location, string fileName, string fileVersion)
        {
            XmlDocument document = new XmlDocument();
            XmlElement clientPaths = null;
            bool exists = false;
            if (File.Exists(location))
            {
                document.Load(location);
                clientPaths = document["clientPaths"];
                foreach (XmlElement clientPath in clientPaths)
                {
                    if (clientPath.GetAttribute("location").Equals(fileName))
                    {
                        if (document["clientPaths"].FirstChild != clientPath)
                        {
                            document["clientPaths"].RemoveChild(clientPath);
                            document["clientPaths"].InsertBefore(
                                clientPath,
                                document["clientPaths"].FirstChild);
                        }
                        document.Save(location);
                        if (!clientPath.GetAttribute("version").Equals(fileVersion))
                        {
                            clientPath.SetAttribute("version", fileVersion);
                            document.Save(location);
                            exists = true;
                            break;
                        }
                        exists = true;
                        break;
                    }
                }
            }
            else
            {
                XmlDeclaration Declaration = document.CreateXmlDeclaration("1.0", "", "");
                document.AppendChild(Declaration);
                clientPaths = document.CreateElement("clientPaths");
                document.AppendChild(clientPaths);
            }
            if (!exists)
            {
                XmlElement clientPath = document.CreateElement("clientPath");
                XmlAttribute locationAttr = document.CreateAttribute("location");
                locationAttr.InnerText = fileName;
                XmlAttribute version = document.CreateAttribute("version");
                version.InnerText = fileVersion;

                clientPath.Attributes.Append(locationAttr);
                clientPath.Attributes.Append(version);
                clientPaths.AppendChild(clientPath);

                if (!Directory.Exists(Constants.TAConstants.AppDataPath))
                    Directory.CreateDirectory(Constants.TAConstants.AppDataPath);

                document.Save(location);
            }
        }
    }
}
