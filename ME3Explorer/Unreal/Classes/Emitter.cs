﻿//This class was generated by ME3Explorer
//Author: Warranty Voider
//URL: http://sourceforge.net/projects/me3explorer/
//URL: http://me3explorer.freeforums.org/
//URL: http://www.facebook.com/pages/Creating-new-end-for-Mass-Effect-3/145902408865659
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ME3Explorer.Unreal;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using KFreonLibGeneral.Debugging;
using BitConverter = KFreonLibGeneral.Misc.BitConverter;

namespace ME3Explorer.Unreal.Classes
{
    public class Emitter
    {
        #region Unreal Props

        //Byte Properties

        public int Physics;
        public int TickGroup;
        //Bool Properties

        public bool bNoVFXSound = false;
        public bool bHidden = false;
        public bool bShadowParented = false;
        public bool bCanStepUpOn = false;
        public bool bPathColliding = false;
        public bool bHardAttach = false;
        public bool bBioSnapToBase = false;
        public bool bCurrentlyActive = false;
        public bool bIgnoreBaseRotation = false;
        public bool bLockLocation = false;
        public bool bPostUpdateTickGroup = false;
        //Name Properties

        public int Tag;
        public int Group;
        public int BaseBoneName;
        public int UniqueTag;
        //Object Properties

        public int ParticleSystemComponent;
        public int LightEnvironment;
        public int Base;
        public int BaseSkelComponent;
        //Float Properties

        public float DrawScale;
        public float CreationTime;
        //Vector3 Properties

        public Vector3 location;

        #endregion

        public int MyIndex;
        public PCCObject pcc;
        public byte[] data;
        public List<PropertyReader.Property> Props;
        public Matrix MyMatrix;
        public CustomVertex.PositionColored[] points;
        public CustomVertex.PositionColored[] points_sel;
        public bool isSelected = false;
        public bool isEdited = false;

        public Emitter(PCCObject Pcc, int Index)
        {
            pcc = Pcc;
            MyIndex = Index;
            if (pcc.isExport(Index))
                data = pcc.Exports[Index].Data;
            Props = PropertyReader.getPropList(pcc, data);
            BitConverter.IsLittleEndian = true;
            foreach (PropertyReader.Property p in Props)
                switch (pcc.getNameEntry(p.Name))
                {
                    #region
                    case "Physics":
                        Physics = p.Value.IntValue;
                        break;
                    case "TickGroup":
                        TickGroup = p.Value.IntValue;
                        break;
                    case "bNoVFXSound":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bNoVFXSound = true;
                        break;
                    case "bHidden":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bHidden = true;
                        break;
                    case "bShadowParented":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bShadowParented = true;
                        break;
                    case "bCanStepUpOn":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bCanStepUpOn = true;
                        break;
                    case "bPathColliding":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bPathColliding = true;
                        break;
                    case "bHardAttach":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bHardAttach = true;
                        break;
                    case "bBioSnapToBase":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bBioSnapToBase = true;
                        break;
                    case "bCurrentlyActive":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bCurrentlyActive = true;
                        break;
                    case "bIgnoreBaseRotation":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bIgnoreBaseRotation = true;
                        break;
                    case "bLockLocation":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bLockLocation = true;
                        break;
                    case "bPostUpdateTickGroup":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bPostUpdateTickGroup = true;
                        break;
                    case "Tag":
                        Tag = p.Value.IntValue;
                        break;
                    case "Group":
                        Group = p.Value.IntValue;
                        break;
                    case "BaseBoneName":
                        BaseBoneName = p.Value.IntValue;
                        break;
                    case "UniqueTag":
                        UniqueTag = p.Value.IntValue;
                        break;
                    case "ParticleSystemComponent":
                        ParticleSystemComponent = p.Value.IntValue;
                        break;
                    case "LightEnvironment":
                        LightEnvironment = p.Value.IntValue;
                        break;
                    case "Base":
                        Base = p.Value.IntValue;
                        break;
                    case "BaseSkelComponent":
                        BaseSkelComponent = p.Value.IntValue;
                        break;
                    case "DrawScale":
                        DrawScale = BitConverter.ToSingle(p.raw, p.raw.Length - 4);
                        break;
                    case "CreationTime":
                        CreationTime = BitConverter.ToSingle(p.raw, p.raw.Length - 4);
                        break;
                    case "location":
                        location = new Vector3(BitConverter.ToSingle(p.raw, p.raw.Length - 12),
                                              BitConverter.ToSingle(p.raw, p.raw.Length - 8),
                                              BitConverter.ToSingle(p.raw, p.raw.Length - 4));
                        break;
                    #endregion
                }
            MyMatrix = Matrix.Translation(location);
            GenerateMesh();
        }

        public void GenerateMesh()
        {
            List<CustomVertex.PositionColored> list = new List<CustomVertex.PositionColored>();
            float w = 20;
            float h = 100;
            Vector3 z = new Vector3();
            int c = Color.Green.ToArgb();
            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, -w, h), c));

            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, -w, h), c));

            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, -w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, -w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, -w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, -w, h), c));
            points = list.ToArray();
            points_sel = list.ToArray();
            for (int i = 0; i < points_sel.Length; i++)
                points_sel[i].Color = Color.Red.ToArgb();
        }

        public void SaveChanges()
        {
            if (isEdited)
            {
                byte[] buff = Vector3ToBuff(location);
                int f = -1;
                for (int i = 0; i < Props.Count; i++)
                    if (pcc.getNameEntry(Props[i].Name) == "location")
                    {
                        f = i;
                        break;
                    };
                if (f != -1)//has prop
                {
                    int off = Props[f].offend - 12;
                    for (int i = 0; i < 12; i++)
                        data[off + i] = buff[i];
                }
                else//have to add prop
                {
                    DebugOutput.PrintLn(MyIndex + " : cant find location property");
                }
                pcc.Exports[MyIndex].Data = data;
            }
        }

        public byte[] Vector3ToBuff(Vector3 v)
        {
            MemoryStream m = new MemoryStream();
            BitConverter.IsLittleEndian = true;
            m.Write(BitConverter.GetBytes(v.X), 0, 4);
            m.Write(BitConverter.GetBytes(v.Y), 0, 4);
            m.Write(BitConverter.GetBytes(v.Z), 0, 4);
            return m.ToArray();
        }

        public void CreateModJobs()
        {
            if (isEdited)
            {
                byte[] buff = Vector3ToBuff(location);
                int f = -1;
                for (int i = 0; i < Props.Count; i++)
                    if (pcc.getNameEntry(Props[i].Name) == "location")
                    {
                        f = i;
                        break;
                    };
                if (f != -1)//has prop
                {
                    int off = Props[f].offend - 12;
                    for (int i = 0; i < 12; i++)
                        data[off + i] = buff[i];
                }
                else//have to add prop
                {
                    DebugOutput.PrintLn(MyIndex + " : cant find location property");
                }
                KFreonLibME.Scripting.ModMaker.ModJob mj = new KFreonLibME.Scripting.ModMaker.ModJob();
                string currfile = Path.GetFileName(pcc.pccFileName);
                mj.data = data;
                mj.Name = "Binary Replacement for file \"" + currfile + "\" in Object #" + MyIndex + " with " + data.Length + " bytes of data";
                string lc = Path.GetDirectoryName(Application.ExecutablePath);
                string template = System.IO.File.ReadAllText(lc + "\\exec\\JobTemplate_Binary2.txt");
                template = template.Replace("**m1**", MyIndex.ToString());
                template = template.Replace("**m2**", currfile);
                mj.Script = template;
                KFreonLibME.Scripting.ModMakerHelper.JobList.Add(mj);
                DebugOutput.PrintLn("Created Mod job : " + mj.Name);
            }
        }

        public void ProcessTreeClick(int[] path, bool AutoFocus)
        {
            isSelected = true;
        }

        public void ApplyTransform(Matrix m)
        {
            if (isSelected)
            {
                location += new Vector3(m.M41, m.M42, m.M43);
                MyMatrix = Matrix.Translation(location);
                isEdited = true;
            }
        }

        public void SetSelection(bool Selected)
        {
            isSelected = Selected;
        }

        public void Render(Device device)
        {
            device.RenderState.Lighting = false;
            device.Transform.World = MyMatrix;
            device.VertexFormat = CustomVertex.PositionColored.Format;
            if (points.Length != 0 && !isSelected)
                device.DrawUserPrimitives(PrimitiveType.LineList, points.Length / 2, points);
            if (points_sel.Length != 0 && isSelected)
                device.DrawUserPrimitives(PrimitiveType.LineList, points_sel.Length / 2, points_sel);
        }

        public TreeNode ToTree()
        {
            TreeNode res = new TreeNode(pcc.Exports[MyIndex].ObjectName + "(#" + MyIndex + ")");
            res.Nodes.Add("Physics : " + pcc.getNameEntry(Physics));
            res.Nodes.Add("TickGroup : " + pcc.getNameEntry(TickGroup));
            res.Nodes.Add("bNoVFXSound : " + bNoVFXSound);
            res.Nodes.Add("bHidden : " + bHidden);
            res.Nodes.Add("bShadowParented : " + bShadowParented);
            res.Nodes.Add("bCanStepUpOn : " + bCanStepUpOn);
            res.Nodes.Add("bPathColliding : " + bPathColliding);
            res.Nodes.Add("bHardAttach : " + bHardAttach);
            res.Nodes.Add("bBioSnapToBase : " + bBioSnapToBase);
            res.Nodes.Add("bCurrentlyActive : " + bCurrentlyActive);
            res.Nodes.Add("bIgnoreBaseRotation : " + bIgnoreBaseRotation);
            res.Nodes.Add("bLockLocation : " + bLockLocation);
            res.Nodes.Add("bPostUpdateTickGroup : " + bPostUpdateTickGroup);
            res.Nodes.Add("Tag : " + pcc.getNameEntry(Tag));
            res.Nodes.Add("Group : " + pcc.getNameEntry(Group));
            res.Nodes.Add("BaseBoneName : " + pcc.getNameEntry(BaseBoneName));
            res.Nodes.Add("UniqueTag : " + pcc.getNameEntry(UniqueTag));
            res.Nodes.Add("ParticleSystemComponent : " + ParticleSystemComponent);
            res.Nodes.Add("LightEnvironment : " + LightEnvironment);
            res.Nodes.Add("Base : " + Base);
            res.Nodes.Add("BaseSkelComponent : " + BaseSkelComponent);
            res.Nodes.Add("DrawScale : " + DrawScale);
            res.Nodes.Add("CreationTime : " + CreationTime);
            return res;
        }

    }
}