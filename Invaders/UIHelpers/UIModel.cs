using System;

namespace Invaders.UIHelpers
{
    internal sealed class UiModel
    {
        public string Name { get; set; } = string.Empty;
        public string Army { get; set; } = string.Empty;
        public string Wheat { get; set; } = string.Empty;
        public string Wood { get; set; } = string.Empty;
        public string Gold { get; set; } = string.Empty;

        public string Turn { get; set; } = string.Empty;

        public string BuildingType { get; set; } = string.Empty;
        public string BuildingNum { get; set; } = string.Empty;
        public string BuildingResWheat { get; set; } = string.Empty;
        public string BuildingResWood { get; set; } = string.Empty;
        public string BuildingResGold { get; set; } = string.Empty;
        
        public string WariorType { get; set; } = string.Empty;
        public string WariorDistance { get; set; } = string.Empty;
        public string WariorOwner { get; set; } = string.Empty;
        public string WariorAttackDistance { get; set; } = string.Empty;
        public string WariorAttacking { get; set; } = string.Empty;
        public string WariorAttackRate { get; set; } = string.Empty;
        
        public string HexType { get; set; } = string.Empty;
        public string HexOwner { get; set; } = string.Empty;
        public string HexWarior { get; set; } = string.Empty;
        public string HexBuild { get; set; } = string.Empty;
    }
}
