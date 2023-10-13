using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCORE
{
    public static class PlayerStateHelper
    {
        public static bool isAttacking { get; set; } = false;
        public static bool isDodging { get; set; } = false;
        public static bool isDamaged { get; set; } = false;
        public static bool isKnockedDown { get; set; } = false;
        public static bool isTalking = false;
    }
}
