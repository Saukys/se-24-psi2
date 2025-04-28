using System;

namespace se_24.frontend.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

    public class GameStat
    {
        public string GameName { get; set; }
        public int HighScore { get; set; }
        public int GamesPlayed { get; set; }
        public DateTime? LastPlayed { get; set; }
    }

    public class AuthState
    {
        public string Username { get; set; }
        public Guid UserId { get; set; }
    }
} 