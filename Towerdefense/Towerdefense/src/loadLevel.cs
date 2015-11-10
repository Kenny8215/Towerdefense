using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class LoadLevel
    {
        private SortedList<int, Wave> waves = null;

        private int[,,] grid;

        public void load(String str)
        {
            /* var uri = new System.Uri(str);
             var converted = uri.AbsoluteUri;

             XmlTextReader read = new XmlTextReader(converted);

             read.ReadToFollowing("wave");

             */

            string note = "";
            string prevNote = "";

            int x = 0;
            int y = 0;
            string type = "";
            int rot = 0;

            Enemy currEnemy = null;
            Wave currWave = null;            

            int waveCount = 0;

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

                                prevNote = note;
                                note = reader.Name;    
                                                   
                            break;


                            case XmlNodeType.Text:
                            //the text in the element
                            switch (note)
                            {
                                case "level":
                                    break;

                                case "waves":
                                    waves = new SortedList<int, Wave>();
                                    break;

                                case "wave":
                                    currWave = new Wave();
                                    waveCount++;
                                    break;

                                case "count":

                                    if (prevNote == "wave")
                                    {
                                        currWave.Count = Int32.Parse(reader.Value);
                                    }else if (prevNote == "grid")
                                    {
                                        int tmp = Int32.Parse(reader.Value);
                                        grid = new int[tmp, tmp, 2];
                                    }

                                    break;

                                case "enemy":
                                    currEnemy = new Enemy();
                                    break;

                                case "name":
                                    //currEnemy.ID1 = reader.Value;
                                    break;

                                case "hitPoints":
                                    currEnemy.HitPoints = Int32.Parse(reader.Value);
                                    break;

                                case "movementSpeed":
                                    currEnemy.MovementSpeed = Int32.Parse(reader.Value);
                                    break;

                                case "sprite":
                                    //currEnemy.setSprite(reader.Value);
                                    break;

                                case "resistance":
                                    currEnemy.Resistance = reader.Value;
                                    break;

                                case "boss":
                                    if (reader.Value == "false")
                                    {
                                        currEnemy.IsBoss = false;
                                    }
                                    else
                                    {
                                        currEnemy.IsBoss = true;
                                    }
                                    break;

                                case "flying":
                                    if (reader.Value == "false")
                                    {
                                        currEnemy.IsFlying = false;
                                    }
                                    else
                                    {
                                        currEnemy.IsFlying = true;
                                    }
                                    break;

                                case "grid":
                                    break;

                                case "field":
                                    break;

                                case "type":
                                    type = reader.Value;
                                    break;

                                case "rotation":
                                    rot = Int32.Parse(reader.Value);
                                    break;

                                case "x":
                                    x = Int32.Parse(reader.Value);
                                    break;

                                case "y":
                                    y = Int32.Parse(reader.Value);
                                    break;
                            }
                            break;
                            case XmlNodeType.XmlDeclaration:

                            break;

                            case XmlNodeType.ProcessingInstruction:
                                //no idea
                                break;

                            case XmlNodeType.Comment:
                                //comment
                                break;

                            case XmlNodeType.EndElement:
                            //the end of an element

                            switch (reader.Name)
                            {
                                case "/level":
                                    break;

                                case "/waves":
                                    break;

                                case "/wave":
                                    waves.Add(waveCount, currWave);
                                    break;

                                case "/count":
                                    break;

                                case "/enemy":
                                    currWave.Enemy = currEnemy;
                                    break;

                                case "/name":
                                    break;

                                case "/hitPoints":
                                    break;

                                case "/movementSpeed":
                                    break;

                                case "/sprite":
                                    break;

                                case "/resistance":
                                    break;

                                case "/boss":
                                    break;

                                case "/flying":
                                    break;

                                case "/grid":
                                    break;

                                case "/field":
                                    if (type == "start")
                                    {
                                        grid[x, y, 0] = 1;
                                    }
                                    
                                    grid[x, y, 1] = rot;
                                    break;

                                case "/type":
                                    break;

                                case "/rotation":
                                    break;
                            }


                            break;
                        }
                    }
                
            }
        }

        public int[,,] getGrid()
        {
            return grid;
        }

        public SortedList<int,Wave> getWaves()
        {

            return waves;            

        }
    }
}
