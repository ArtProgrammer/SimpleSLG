using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.Spatial
{
    public interface ISpatialMember
    {
        int SpatialNodeID
        {
            set; get;
        }

        void MemberIsDirty();

        void HandleDestory();
    }
}