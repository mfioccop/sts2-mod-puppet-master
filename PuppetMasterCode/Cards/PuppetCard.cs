using MegaCrit.Sts2.Core.Entities.Cards;

namespace PuppetMaster.PuppetMasterCode.Cards;

public abstract class PuppetCard(int cost, CardType type, CardRarity rarity, TargetType target) : PuppetMasterCard(cost, type, rarity, target);