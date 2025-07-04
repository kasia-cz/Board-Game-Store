﻿using BoardGameStore.Domain.Enums;

namespace BoardGameStore.Domain.Models
{
    public class BoardGameModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public GameDifficulty Difficulty { get; set; }

        public int AvailableQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
