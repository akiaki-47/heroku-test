﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ProgramManagerStudent
{
    class Room
    {
        /// <summary>
        /// generate uuid, no, numbOfStudent
        /// </summary>
        private string uuid;
        private uint no;
        private uint numbOfStudent;
        
        /// <value>uuid</value>
        public string Uuid { get { return uuid; }}
        
        /// <value>getNo</value>
        public uint No { get { return no; }}
        /// <value>Get NumbOfStudent </value>
        public uint NumbOfStudent { get { return numbOfStudent; } set { numbOfStudent = value; } }

        /// <summary>
        /// Generate Constructor Room
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="no"></param>
        /// <param name="numbOfStudent"></param>
        public Room(string uuid, uint no, uint numbOfStudent)
        {
            this.uuid = uuid;
            this.no = no;
            this.numbOfStudent = numbOfStudent;
        }

        /// <summary>
        /// Generate Room without parameter
        /// </summary> 
        public Room()
        {
        }

        /// <summary>
        /// Create a Room with number of students
        /// </summary>
        /// <param name="numbOfStudentInSchool"></param>
        /// <returns>list room</returns>
        public static List<Room> CreateRoom(uint numOfRoom, uint numbOfStudentInSchool)
        {
            Random rnd = new Random();
            List<Room> roomList = new List<Room>();
            for(uint i = 0; i < numOfRoom; i++)
            {
                string uuid = Guid.NewGuid().ToString();
                roomList.Add(new Room(uuid, i+1, 30));
            }
            numbOfStudentInSchool -= numOfRoom * 30;
            while (numbOfStudentInSchool > 0)
            {
                foreach (Room room in roomList)
                {
                    uint temp = (uint)rnd.Next(0, 2);
                    if (numbOfStudentInSchool > 0)
                    {
                        if(room.numbOfStudent < 50)
                        {
                            room.numbOfStudent += temp;
                            numbOfStudentInSchool -= temp;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return roomList;
        }// end of CreateRoom

        public static List<Room> CreateRoomByRoomNum(uint numOfRoom)
        {
            Random rnd = new Random();
            List<Room> roomList = new List<Room>();
            int minStudentNum = (int)numOfRoom * 30;
            int maxStudentNum = (int)numOfRoom * 50;
            uint numOfStudentInSchool = (uint)rnd.Next(minStudentNum, maxStudentNum + 1);
            roomList = CreateRoom(numOfRoom, numOfStudentInSchool);
            return roomList;
        }// end of CreateRoomByRoomNum
        public static List<Room> CreateRoomByStudentNum(uint numbOfStudentInSchool)
        {
            Random rnd = new Random();
            List<Room> roomList = new List<Room>();
            int minRoom = (int)numbOfStudentInSchool / 50;
            int maxRoom = (int)numbOfStudentInSchool / 30;
            uint numOfRoom = (uint)rnd.Next(minRoom, maxRoom + 1);

            roomList = CreateRoom(numOfRoom, numbOfStudentInSchool);
            return roomList;
        }// end of CreateRoomByStudentNum

        /// <summary>
        /// Save to file.
        /// </summary>
        /// <param name="roomList"></param>
        public static void SaveRooms(List<Room> roomList)
        {
            String content = "UUID, No, Number Of Students";
            foreach (Room room in roomList)
            {
                content += "\n" + room.Uuid + ", " + room.No + ", " + room.NumbOfStudent;
            }
            File.WriteAllText(@"..\..\..\Rooms.csv", content);
        }//end of SaveRooms
        public static uint GetAllStudent(List<Room> roomList)
        {
            uint numOfStudent = 0;
            foreach(Room room in roomList)
            {
                numOfStudent += room.NumbOfStudent;
            }
            return numOfStudent;
        }
    }
}
