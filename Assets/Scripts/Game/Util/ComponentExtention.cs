using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public static class ComponentExtention
    {
        public static bool CompareTag(this Component component, TagType tagType)
        {
            string tagName = GetTagString(tagType);
            return component.CompareTag(tagName);
        }

        private static string GetTagString(TagType tagType)
            => tagType switch
            {
                TagType.Player => "Player",
                TagType.Enemy => "Enemy",
                TagType.PlayerBullet => "PlayerBullet",
                TagType.EnemyBullet => "EnemyBullet",
                _ => throw new ArgumentOutOfRangeException(nameof(tagType), $"Not expected direction value: {tagType}")
            };
    }
}

