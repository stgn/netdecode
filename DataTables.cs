using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace netdecode
{
    class DataTables
    {
        enum SendPropType : uint
        {
            Int = 0,
            Float,
            Vector,
            String,
            Array,
            DataTable
        }

        [Flags]
        enum SendPropFlags : uint
        {
            UNSIGNED = 1,
            COORD = 2,
            NOSCALE = 4,
            ROUNDDOWN = 8,
            ROUNDUP = 16,
            NORMAL = 32,
            EXCLUDE = 64,
            XYZE = 128,
            INSIDEARRAY = 256,
            PROXY_ALWAYS_YES = 512,
            CHANGES_OFTEN = 1024,
            IS_A_VECTOR_ELEM = 2048,
            COLLAPSIBLE = 4096,
            COORD_MP = 8192,
            COORD_MP_LOWPRECISION = 16384,
            COORD_MP_INTEGRAL = 32768,
            ENCODED_AGAINST_TICKCOUNT = 65536
        }

        public static void Parse(byte[] data, TreeNode node)
        {
            var bb = new BitBuffer(data);

            while (bb.ReadBool())
            {
                bool needsdecoder = bb.ReadBool();
                var dtnode = node.Nodes.Add(bb.ReadString());
                var numprops = bb.ReadBits(10);

                for (int i = 0; i < numprops; i++)
                {
                    var type = (SendPropType)bb.ReadBits(5);
                    dtnode.Nodes.Add("DPT_" + type + " " + bb.ReadString());
                    var flags = (SendPropFlags)bb.ReadBits(16);

                    if (type == SendPropType.DataTable)
                        bb.ReadString();
                    else {
                        if ((flags & SendPropFlags.EXCLUDE) != 0)
                            bb.ReadString();
                        else if (type == SendPropType.Array)
                            bb.Seek(10);
                        else
                            bb.Seek(70);
                    }
                }
            }
        }
    }
}
