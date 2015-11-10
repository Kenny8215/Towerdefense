using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class LoadLevel
    {
        public void load(String str)
        {
            /* var uri = new System.Uri(str);
             var converted = uri.AbsoluteUri;

             XmlTextReader read = new XmlTextReader(converted);

             read.ReadToFollowing("wave");

             */
            System.IO.StreamReader XMLread = new System.IO.StreamReader(@str);

            using (XmlReader reader = XmlReader.Create(XMLread))
            {
                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                //startelement
                                break;
                            case XmlNodeType.Text:
                                //the text in the element
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                //no idea
                                break;
                            case XmlNodeType.Comment:
                                //comment
                                break;
                            case XmlNodeType.EndElement:
                                //the end of an element
                                break;
                        }
                    }
                
            }
        }

        public int[,] getGrid()
        {
            int[,] ret = new int[1,1];

            return ret;
        }

        public SortedList<int,List<Enemy>> getWaves()
        {
            SortedList<int, List<Enemy>> ret = new SortedList<int, List<Enemy>>();

            return ret;

        }
    }
}
