﻿//This class was generated by ME3Explorer
//Author: Warranty Voider
//URL: http://sourceforge.net/projects/me3explorer/
//URL: http://me3explorer.freeforums.org/
//URL: http://www.facebook.com/pages/Creating-new-end-for-Mass-Effect-3/145902408865659
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ME3Explorer.Unreal;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using BitConverter = KFreonLibGeneral.Misc.BitConverter;

namespace ME3Explorer.Unreal.Classes
{
    public class AnimNodeSlot
    {
        #region Unreal Props

        //Bool Properties

        public bool bSkipTickWhenZeroWeight = false;
        //Name Properties

        public int NodeName;
        //Float Properties

        public float NodeTotalWeight;
        //Array Properties

        public List<ChildrenEntry> Children;

        public struct ChildrenEntry
        {
            public string Name;
            public float Weight;
            public int Anim;
            public bool bMirrorSkeleton;
            public bool bIsAdditive;
        }

        #endregion

        public int MyIndex;
        public PCCObject pcc;
        public byte[] data;
        public List<PropertyReader.Property> Props;

        public AnimNodeSlot(PCCObject Pcc, int Index)
        {
            pcc = Pcc;
            MyIndex = Index;
            if (pcc.isExport(Index))
                data = pcc.Exports[Index].Data;
            Props = PropertyReader.getPropList(pcc, data);
            BitConverter.IsLittleEndian = true;
            Children = new List<ChildrenEntry>();            
            foreach (PropertyReader.Property p in Props)
                switch (pcc.getNameEntry(p.Name))
                {

                    case "bSkipTickWhenZeroWeight":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bSkipTickWhenZeroWeight = true;
                        break;
                    case "NodeName":
                        NodeName = p.Value.IntValue;
                        break;
                    case "NodeTotalWeight":
                        NodeTotalWeight = BitConverter.ToSingle(p.raw, p.raw.Length - 4);
                        break;
                    case "Children":
                        ReadChildren(p.raw);
                        break;
                }
        }

        public void ReadChildren(byte[] raw)
        {
            int count = GetArrayCount(raw);
            byte[] buff = GetArrayContent(raw);
            int pos = 0;
            for (int i = 0; i < count; i++)
            {
                List<PropertyReader.Property> pp = PropertyReader.ReadProp(pcc, buff, pos);
                pos = pp[pp.Count - 1].offend;
                ChildrenEntry e = new ChildrenEntry();
                foreach (PropertyReader.Property p in pp)
                    switch (pcc.getNameEntry(p.Name))
                    {
                        case "Name":
                            e.Name = pcc.getNameEntry(p.Value.IntValue);
                            break;
                        case "Weight":
                            e.Weight = BitConverter.ToSingle(p.raw, p.raw.Length - 4);
                            break;
                        case "Anim":
                            e.Anim = p.Value.IntValue;
                            break;
                        case "bMirrorSkeleton":
                            e.bMirrorSkeleton = (p.raw[p.raw.Length - 1] == 1);
                            break;
                        case "bIsAdditive":
                            e.bIsAdditive = (p.raw[p.raw.Length - 1] == 1);
                            break;
                    }
                Children.Add(e);
            }
        }

        public int GetArrayCount(byte[] raw)
        {
            return BitConverter.ToInt32(raw, 24);
        }

        public byte[] GetArrayContent(byte[] raw)
        {
            byte[] buff = new byte[raw.Length - 28];
            for (int i = 0; i < raw.Length - 28; i++)
                buff[i] = raw[i + 28];
            return buff;
        }

        public TreeNode ToTree()
        {
            TreeNode res = new TreeNode(pcc.Exports[MyIndex].ObjectName + "(#" + MyIndex + ")");
            res.Nodes.Add("bSkipTickWhenZeroWeight : " + bSkipTickWhenZeroWeight);
            res.Nodes.Add("NodeName : " + pcc.getNameEntry(NodeName));
            res.Nodes.Add("NodeTotalWeight : " + NodeTotalWeight);
            res.Nodes.Add(ChildrenToTree());
            return res;
        }

        public TreeNode ChildrenToTree()
        {
            TreeNode res = new TreeNode("Children");
            for (int i = 0; i < Children.Count; i++)
            {
                int idx = Children[i].Anim;
                TreeNode t = new TreeNode(i.ToString());
                t.Nodes.Add("Name : " + Children[i].Name);
                t.Nodes.Add("Weight : " + Children[i].Weight);
                t.Nodes.Add("Anim : " + Children[i].Anim);
                if (pcc.isExport(idx))
                    switch (pcc.Exports[idx].ClassName)
                    {
                        case "AnimNodeSlot":
                            AnimNodeSlot ans = new AnimNodeSlot(pcc, idx);
                            t.Nodes.Add(ans.ToTree());
                            break;
                    }
                t.Nodes.Add("bIsMirrorSkeleton : " + Children[i].bMirrorSkeleton);
                t.Nodes.Add("bIsAdditive : " + Children[i].bIsAdditive);
                res.Nodes.Add(t);
            }
            return res;
        }

    }
}