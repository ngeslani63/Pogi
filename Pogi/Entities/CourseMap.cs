﻿using Pogi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class CourseMap
    {
        public CourseMap ()
        {
            Lat01 = 40.460426;
            Lng01 = -74.640557;
            InitLat01 = 40.461444;
            InitLng01 = -74.640231;
            Hdg01 = 180;
            Zoom01 = 18;

            Lat02 = 40.462924;
            Lng02 = -74.640620;
            InitLat02 = 40.461695;
            InitLng02 = -74.640909;
            Hdg02 = 20;
            Zoom02 = 18;

            Lat03 = 40.461311;
            Lng03 = -74.641654;
            InitLat03 = 40.461874;
            InitLng03 = -74.641596;
            Hdg03 = 180;
            Zoom03 = 19;
            //4
            Lat04 = 40.461811;
            Lng04 = -74.644780;
            InitLat04 = 40.461395;
            InitLng04 = -74.643329;
            Hdg04 = 270;
            Zoom04 = 18;
            //5
            Lat05 = 40.465163;
            Lng05 = -74.647966;
            InitLat05 = 40.464206;
            InitLng05 = -74.647461;
            Hdg05 = 25;
            Zoom05 = 18;
            //6
            Lat06 = 40.465987;
            Lng06 = -74.645733;
            InitLat06 = 40.465997;
            InitLng06 = -74.646530;
            Hdg06 = 100;
            Zoom06 = 19;
            //7
            Lat07 = 40.465030;
            Lng07 = -74.640350;
            InitLat07 = 40.465126;
            InitLng07 = -74.641938;
            Hdg07 = 100;
            Zoom07 = 18;
            //8
            Lat08 = 40.465239;
            Lng08 = -74.645198;
            InitLat08 = 40.464948;
            InitLng08 = -74.644096;
            Hdg08 = 270;
            Zoom08 = 18;
            //9
            Lat09 = 40.463770;
            Lng09 = -74.641090;
            InitLat09 = 40.463973;
            InitLng09 = -74.642615;
            Hdg09 = 90;
            Zoom09 = 18;

            Zoom = 18;

        }
        public int CourseMapId { get; set; }
        public int CourseId { get; set; }
        public double CourseLat { get; set; }
        public double CourseLng { get; set; }
        public short Zoom { get; set; }
        public double Lat01 { get; set; }
        public double Lat02 { get; set; }
        public double Lat03 { get; set; }
        public double Lat04 { get; set; }
        public double Lat05 { get; set; }
        public double Lat06 { get; set; }
        public double Lat07 { get; set; }
        public double Lat08 { get; set; }
        public double Lat09 { get; set; }
        public double Lat10 { get; set; }
        public double Lat11 { get; set; }
        public double Lat12 { get; set; }
        public double Lat13 { get; set; }
        public double Lat14 { get; set; }
        public double Lat15 { get; set; }
        public double Lat16 { get; set; }
        public double Lat17 { get; set; }
        public double Lat18 { get; set; }
        public double Lng01 { get; set; }
        public double Lng02 { get; set; }
        public double Lng03 { get; set; }
        public double Lng04 { get; set; }
        public double Lng05 { get; set; }
        public double Lng06 { get; set; }
        public double Lng07 { get; set; }
        public double Lng08 { get; set; }
        public double Lng09 { get; set; }
        public double Lng10 { get; set; }
        public double Lng11 { get; set; }
        public double Lng12 { get; set; }
        public double Lng13 { get; set; }
        public double Lng14 { get; set; }
        public double Lng15 { get; set; }
        public double Lng16 { get; set; }
        public double Lng17 { get; set; }
        public double Lng18 { get; set; }
        public short Hdg01 { get; set; }
        public short Hdg02 { get; set; }
        public short Hdg03 { get; set; }
        public short Hdg04 { get; set; }
        public short Hdg05 { get; set; }
        public short Hdg06 { get; set; }
        public short Hdg07 { get; set; }
        public short Hdg08 { get; set; }
        public short Hdg09 { get; set; }
        public short Hdg10 { get; set; }
        public short Hdg11 { get; set; }
        public short Hdg12 { get; set; }
        public short Hdg13 { get; set; }
        public short Hdg14 { get; set; }
        public short Hdg15 { get; set; }
        public short Hdg16 { get; set; }
        public short Hdg17 { get; set; }
        public short Hdg18 { get; set; }
        public double InitLat01 { get; set; }
        public double InitLat02 { get; set; }
        public double InitLat03 { get; set; }
        public double InitLat04 { get; set; }
        public double InitLat05 { get; set; }
        public double InitLat06 { get; set; }
        public double InitLat07 { get; set; }
        public double InitLat08 { get; set; }
        public double InitLat09 { get; set; }
        public double InitLat10 { get; set; }
        public double InitLat11 { get; set; }
        public double InitLat12 { get; set; }
        public double InitLat13 { get; set; }
        public double InitLat14 { get; set; }
        public double InitLat15 { get; set; }
        public double InitLat16 { get; set; }
        public double InitLat17 { get; set; }
        public double InitLat18 { get; set; }
        public double InitLng01 { get; set; }
        public double InitLng02 { get; set; }
        public double InitLng03 { get; set; }
        public double InitLng04 { get; set; }
        public double InitLng05 { get; set; }
        public double InitLng06 { get; set; }
        public double InitLng07 { get; set; }
        public double InitLng08 { get; set; }
        public double InitLng09 { get; set; }
        public double InitLng10 { get; set; }
        public double InitLng11 { get; set; }
        public double InitLng12 { get; set; }
        public double InitLng13 { get; set; }
        public double InitLng14 { get; set; }
        public double InitLng15 { get; set; }
        public double InitLng16 { get; set; }
        public double InitLng17 { get; set; }
        public double InitLng18 { get; set; }
        public short Zoom01 { get; set; }
        public short Zoom02 { get; set; }
        public short Zoom03 { get; set; }
        public short Zoom04 { get; set; }
        public short Zoom05 { get; set; }
        public short Zoom06 { get; set; }
        public short Zoom07 { get; set; }
        public short Zoom08 { get; set; }
        public short Zoom09 { get; set; }
        public short Zoom10 { get; set; }
        public short Zoom11 { get; set; }
        public short Zoom12 { get; set; }
        public short Zoom13 { get; set; }
        public short Zoom14 { get; set; }
        public short Zoom15 { get; set; }
        public short Zoom16 { get; set; }
        public short Zoom17 { get; set; }
        public short Zoom18 { get; set; }

    }
}
