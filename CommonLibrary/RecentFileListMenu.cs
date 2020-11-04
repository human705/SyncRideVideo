using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    class RecentFileListMenu
    {
        //Queue<string> MRUlist = new Queue<string>();
        //int MRUnumber = -1;
        ////private void SaveRecentFile(string path)
        ////{
        ////    //clear all recent list from menu
        ////    recentToolStripMenuItem.DropDownItems.Clear();
        ////    LoadRecentList(); //load list from file
        ////    if (!(MRUlist.Contains(path))) //prevent duplication on recent list
        ////        MRUlist.Enqueue(path); //insert given path into list
        ////                               //keep list number not exceeded the given value
        ////    while (MRUlist.Count > MRUnumber)
        ////    {
        ////        MRUlist.Dequeue();
        ////    }
        ////    foreach (string item in MRUlist)
        ////    {
        ////        //create new menu for each item in list
        ////        ToolStripMenuItem fileRecent = new ToolStripMenuItem
        ////                     (item, null, RecentFile_click);
        ////        //add the menu to "recent" menu
        ////        recentToolStripMenuItem.DropDownItems.Add(fileRecent);
        ////    }
        ////    //writing menu list to file
        ////    //create file called "Recent.txt" located on app folder
        ////    StreamWriter stringToWrite =
        ////    new StreamWriter(System.Environment.CurrentDirectory + "\\Recent.txt");
        ////    foreach (string item in MRUlist)
        ////    {
        ////        stringToWrite.WriteLine(item); //write list to stream
        ////    }
        ////    stringToWrite.Flush(); //write stream to file
        ////    stringToWrite.Close(); //close the stream and reclaim memory
        ////}

        ////private void LoadRecentList()
        ////{//try to load file. If file isn't found, do nothing
        ////    MRUlist.Clear();
        ////    try
        ////    {
        ////        //read file stream
        ////        StreamReader listToRead =
        ////      new StreamReader(System.Environment.CurrentDirectory + "\\Recent.txt");
        ////        string line;
        ////        while ((line = listToRead.ReadLine()) != null) //read each line until end of file
        ////            MRUlist.Enqueue(line); //insert to list
        ////        listToRead.Close(); //close the stream
        ////    }
        ////    catch (Exception) { }
        ////}

        ////private void RecentFile_click(object sender, EventArgs e)
        ////{
        ////    //just load a file
        ////    richTextBox1.LoadFile(sender.ToString(), RichTextBoxStreamType.PlainText);
        ////}

    }
}
