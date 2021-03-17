using System.ComponentModel;

namespace BugFablesSaveEditor.BugFablesEnums
{
  public enum SaveFileSection
  {
    Header = 0,
    PartyMembers,
    Global,
    MedalShopsPools,
    MedalShopsAvailables,
    Quests,
    Items,
    Medals,
    SamiraSongs,
    StatBonuses,
    Library,
    Flags,
    Flagstrings,
    Flagvars,
    RegionalFlags,
    CrystalBerries,
    Followers,
    EnemyEncounters,
    COUNT
  }

  public enum Medal
  {
    [Description("HP Plus")]
    HPPlus = 0,
    [Description("TP Plus")]
    TPPlus,
    [Description("Detector")]
    Detector,
    [Description("Last Attack")]
    LastAttack,
    [Description("Last Stand")]
    LastStand,
    [Description("Quick Flea")]
    QuickFlea,
    [Description("Poison Attacker")]
    PoisonAttacker,
    [Description("Poison Resistance")]
    PoisonResistance,
    [Description("Berserker")]
    Berserker,
    [Description("Poison Defender")]
    PoisonDefender,
    [Description("Empower")]
    Empower,
    [Description("Hard Mode")]
    HardMode,
    [Description("Sleep Resistance")]
    SleepResistance,
    [Description("Mighty Pebble")]
    MightyPebble,
    [Description("Spiky Bod")]
    SpikyBod,
    [Description("Break")]
    Break,
    [Description("Break+")]
    BreakPlus,
    [Description("Spy Specs")]
    SpySpecs,
    [Description("Bug Me Not!")]
    BugMeNot,
    [Description("Super Block+")]
    SuperBlockPlus,
    [Description("Favorite One")]
    FavoriteOne,
    [Description("Numb Resistance")]
    NumbResistance,
    [Description("Poison Needles")]
    PoisonNeedles,
    [Description("Strong Start")]
    StrongStart,
    [Description("Weak Stomach")]
    WeakStomach,
    [Description("TP Saver")]
    TPSaver,
    [Description("Reverse Toxin")]
    ReverseToxin,
    [Description("Eternal Venom")]
    EternalVenom,
    [Description("A.D.B.P Enhancer")]
    ADBPEnhancer,
    [Description("Mightier Pebble")]
    MightierPebble,
    [Description("Hard Hits")]
    HardHits,
    [Description("Empower+")]
    EmpowerPlus,
    [Description("Life Stealer")]
    LifeStealer,
    [Description("Freeze Resistance")]
    FreezeResistance,
    [Description("Shock Trooper")]
    ShockTrooper,
    [Description("Front Support")]
    FrontSupport,
    [Description("Back Support")]
    BackSupport,
    [Description("Enfeeble")]
    Enfeeble,
    [Description("Enfeeble+")]
    EnfeeblePlus,
    [Description("Fortify")]
    Fortify,
    [Description("Fortify+")]
    FortifyPlus,
    [Description("Sleepy Needles")]
    SleepyNeedles,
    [Description("EXP Booster")]
    EXPBooster,
    [Description("Status Booster")]
    StatusBooster,
    [Description("Poison Touch")]
    PoisonTouch,
    [Description("Status Relay")]
    StatusRelay,
    [Description("Frostbite")]
    Frostbite,
    [Description("Heavy Sleeper")]
    HeavySleeper,
    [Description("Secure Pouch")]
    SecurePouch,
    [Description("Power Exchange")]
    PowerExchange,
    [Description("Defense Exchange")]
    DefenseExchange,
    [Description("Tardigrade Shield")]
    TardigradeShield,
    [Description("Charge Up")]
    ChargeUp,
    [Description("Charge Up+")]
    ChargeUpPlus,
    [Description("Leaf Cloak")]
    LeafCloak,
    [Description("Random Start")]
    RandomStart,
    [Description("Meditation")]
    Meditation,
    [Description("Electric Needles")]
    ElectricNeedles,
    [Description("Block Heal")]
    BlockHeal,
    [Description("Extra Freeze")]
    ExtraFreeze,
    [Description("Heavy Throw")]
    HeavyThrow,
    [Description("Reflection")]
    Reflection,
    [Description("Prayer")]
    Prayer,
    [Description("Antlion Jaws")]
    AntlionJaws,
    [Description("HP Core")]
    HPCore,
    [Description("TP Core")]
    TPCore,
    [Description("Resist All")]
    ResistAll,
    [Description("Deep Taunt")]
    DeepTaunt,
    [Description("Miracle Matter")]
    MiracleMatter,
    [Description("Victory Buzz")]
    VictoryBuzz,
    [Description("Triumph Buzz")]
    TriumphBuzz,
    [Description("Crazy Prepared")]
    CrazyPrepared,
    [Description("Life Cast")]
    LifeCast,
    [Description("Hard Charge")]
    HardCharge,
    [Description("Heal Plus")]
    HealPlus,
    [Description("Status Mirror")]
    StatusMirror,
    [Description("Luckier Day")]
    LuckierDay,
    [Description("First Plating")]
    FirstPlating,
    [Description("Seedling Affinity")]
    SeedlingAffinity,
    [Description("Berry Finder")]
    BerryFinder,
    COUNT
  }

  public enum Quest
  {
    [Description("No Quests")]
    NOQUEST = 0,
    [Description("Inn Review Required")]
    InnReviewRequired,
    [Description("I Want a New Taste")]
    IWantANewTaste,
    [Description("Theater Help Wanted!")]
    TheaterHelpWanted,
    [Description("Lost Toy")]
    LostToy,
    [Description("My Brother is Gone!")]
    MyBrotherIsGone,
    [Description("Requesting Assistance")]
    RequestingAssistance,
    [Description("Cable Car Bodyguard")]
    CableCarBodyguard,
    [Description("Bounty: Seedling King")]
    BountySeedlingKing,
    [Description("Bounty: False Monarch")]
    BountyFalseMonarch,
    [Description("Bounty: Devourer")]
    BountyDevourer,
    [Description("Ch. 1: A Dysfunctional Trio")]
    Ch1,
    [Description("Ch. 2: Sacred Golden Hills!")]
    Ch2,
    [Description("Ch. 3: Factory Inspection")]
    Ch3,
    [Description("Ch. 4: Mysterious Lost Sands")]
    Ch4,
    [Description("Ch. 5: The Far Wildlands")]
    Ch5,
    [Description("Ch. 6: Assault at Rubber Prison")]
    Ch6,
    [Description("Ch. 7: The Everlasting Sapling")]
    Ch7,
    [Description("I Wanna Get Better!")]
    IWannaGetBetter,
    [Description("My Specialty")]
    MySpecialty,
    [Description("A Smiling Dish")]
    ASmilingDish,
    [Description("Bounty: Tidal Wyrm")]
    BountyTidalWyrm,
    [Description("Ore Wanted")]
    OreWanted,
    [Description("Bounty: Peacock Spider")]
    BountyPeacockSpider,
    [Description("Huuuuuuuuuu...!!!")]
    Huuuuuuuuuu,
    [Description("Lost Item")]
    LostItem,
    [Description("Leif's Request")]
    LeifRequest,
    [Description("Lost Books")]
    LostBooks,
    [Description("Butler Missing!")]
    ButlerMissing,
    [Description("Team Snakemouth... ")]
    TeamSnakemouth,
    [Description("Vi's Request")]
    ViRequest,
    [Description("Power Plant Investigation")]
    PowerPlantInvestigation,
    [Description("Card Masters of Bugaria")]
    CardMastersOfBugaria,
    [Description("Book Return!")]
    BookReturn,
    [Description("I Want a Souvenir... ")]
    IWantASouvenir,
    [Description("Helpers Needed At Once!")]
    HelpersNeededAtOnce,
    [Description("Stolen Item")]
    StolenItem,
    [Description("Rare Item Wanted!")]
    RareItemWanted,
    [Description("Dropped my Hat!")]
    DroppedMyHat,
    [Description("Butler Missing Again!")]
    ButlerMissingAgain,
    [Description("Explorer Check!")]
    ExplorerCheck,
    [Description("Help Me Get it Back!")]
    HelpMeGetItBack,
    [Description("Bandit Hunt")]
    BanditHunt,
    [Description("In Search of Paint... ")]
    InSearchOfPaint,
    [Description("Lunch Delivery!")]
    LunchDelivery,
    [Description("It's Time...!")]
    ItsTime,
    [Description("Want to Relive Memories... ")]
    WantToReliveMemories,
    [Description("Seedling Hunt")]
    SeedlingHunt,
    [Description("Best Friend In The Fog!")]
    BestFriendInTheFog,
    [Description("Parts Delivery")]
    PartsDelivery,
    [Description("Hydration Crisis!")]
    HydrationCrisis,
    [Description("Sweets from Outside!")]
    SweetsfromOutside,
    [Description("Find The Ingredients!")]
    FindTheIngredients,
    [Description("It's Too Hot!")]
    ItsTooHot,
    [Description("In Search of ")]
    InSearchofSomething,
    [Description("Confidential")]
    Confidential,
    [Description("Awful's Beauty")]
    AwfulBeauty,
    [Description("They Took Her...!")]
    TheyTookHer,
    [Description("My Mecha Claw!")]
    MyMechaClaw,
    [Description("Can't Sleep...!")]
    CantSleep,
    [Description("Kabbu's Request")]
    KabbuRequest,
    [Description("Loose Ends")]
    LooseEnds,
    [Description("A New Hope ")]
    ANewHope,
    [Description("Getting Bored ")]
    GettingBored,
    COUNT
  };

  public enum Item
  {
    [Description("Crunchy Leaf")]
    CrunchyLeaf = 0,
    [Description("Honey Drop")]
    HoneyDrop,
    [Description("Spicy Berry")]
    SpicyBerry,
    [Description("Burly Berry")]
    BurlyBerry,
    [Description("Super Pepper")]
    SuperPepper,
    [Description("Iron Seed")]
    IronSeed,
    [Description("RESERVED")]
    RESERVED1,
    [Description("RESERVED")]
    RESERVED2,
    [Description("Mistake")]
    Mistake,
    [Description("Leaf Omelet")]
    LeafOmelet,
    [Description("Honey'd Leaf")]
    HoneyLeaf,
    [Description("Magic Seed")]
    MagicSeed,
    [Description("Clear Water")]
    ClearWater,
    [Description("Mushroom")]
    Mushroom,
    [Description("Cooked Shroom")]
    CookedShroom,
    [Description("Mushroom Salad")]
    MushroomSalad,
    [Description("Leaf Salad")]
    LeafSalad,
    [Description("Aphid Egg")]
    AphidEgg,
    [Description("Fried Egg")]
    FriedEgg,
    [Description("Hearty Breakfast")]
    HeartyBreakfast,
    [Description("Glazed Honey")]
    GlazedHoney,
    [Description("Sweet Shroom")]
    SweetShroom,
    [Description("Sweet Danger")]
    SweetDanger,
    [Description("Hard Seed")]
    HardSeed,
    [Description("Dotted Ball")]
    DottedBall,
    [Description("Bug Ranger Plushie")]
    BugRangerPlushie,
    [Description("Danger Shroom")]
    DangerShroom,
    [Description("Explorer Permit")]
    ExplorerPermit,
    [Description("Cooked Danger")]
    CookedDanger,
    [Description("Coal Crystal")]
    CoalCrystal,
    [Description("Spicy Bomb")]
    SpicyBomb,
    [Description("Poison Bomb")]
    PoisonBomb,
    [Description("Queen's Dinner")]
    QueenDinner,
    [Description("Mushroom Skewer")]
    MushroomSkewer,
    [Description("Roasted Berries")]
    RoastedBerries,
    [Description("Abomihoney")]
    Abomihoney,
    [Description("Clear Bomb")]
    ClearBomb,
    [Description("Ant Compass")]
    AntCompass,
    [Description("Chomper Seed")]
    ChomperSeed,
    [Description("Berry Juice")]
    BerryJuice,
    [Description("Numbnail Dart")]
    NumbnailDart,
    [Description("Map of Bugaria")]
    MapOfBugaria,
    [Description("Magic Ice")]
    MagicIce,
    [Description("Shock Berry")]
    ShockBerry,
    [Description("Frost Bomb")]
    FrostBomb,
    [Description("Numb Bomb")]
    NumbBomb,
    [Description("Sleep Bomb")]
    SleepBomb,
    [Description("Shaved Ice")]
    ShavedIce,
    [Description("Aphid Dew")]
    AphidDew,
    [Description("Honey Ice Cream")]
    HoneyIceCream,
    [Description("Sweet Dew")]
    SweetDew,
    [Description("Ice Cream")]
    IceCream,
    [Description("Lore Book")]
    LoreBook,
    [Description("Cold Salad")]
    ColdSalad,
    [Description("Flower Key")]
    FlowerKey,
    [Description("Sun Offering")]
    SunOffering,
    [Description("Moon Offering")]
    MoonOffering,
    [Description("Mothiva Doll")]
    MothivaDoll,
    [Description("Wooden Crank")]
    WoodenCrank,
    [Description("Big Crank Top Half")]
    BigCrankTopHalf,
    [Description("Big Crank")]
    BigCrank,
    [Description("Big Crank Bottom Half")]
    BigCrankBottomHalf,
    [Description("Abombination")]
    Abombination,
    [Description("Spy Data")]
    SpyData,
    [Description("Danger Spud")]
    DangerSpud,
    [Description("Baked Yam")]
    BakedYam,
    [Description("Spicy Fries")]
    SpicyFries,
    [Description("Burly Chips")]
    BurlyChips,
    [Description("Bag of Flour")]
    BagOfFlour,
    [Description("Yam Bread")]
    YamBread,
    [Description("Spicy Candy")]
    SpicyCandy,
    [Description("Burly Candy")]
    BurlyCandy,
    [Description("Dry Bread")]
    DryBread,
    [Description("Nutty Cake")]
    NuttyCake,
    [Description("Poison Cake")]
    PoisonCake,
    [Description("Shock Candy")]
    ShockCandy,
    [Description("Plain Tea")]
    PlainTea,
    [Description("Tangy Berry")]
    TangyBerry,
    [Description("Tangy Jam")]
    TangyJam,
    [Description("Tangy Juice")]
    TangyJuice,
    [Description("Spicy Tea")]
    SpicyTea,
    [Description("Burly Tea")]
    BurlyTea,
    [Description("Frost Pie")]
    FrostPie,
    [Description("Heart Berry")]
    HeartBerry,
    [Description("Bond Berry")]
    BondBerry,
    [Description("Tangy Pie")]
    TangyPie,
    [Description("Crisbee Donut")]
    CrisbeeDonut,
    [Description("Tangy Carpaccio")]
    TangyCarpaccio,
    [Description("Poison Dart")]
    PoisonDart,
    [Description("Bed Bug")]
    BedBug,
    [Description("Succulent Berry")]
    SucculentBerry,
    [Description("Succulent Platter")]
    SucculentPlatter,
    [Description("Desert Key")]
    DesertKey,
    [Description("Overdue Book")]
    OverdueBook,
    [Description("Pretty Ribbon")]
    PrettyRibbon,
    [Description("Factory Pass")]
    FactoryPass,
    [Description("Agaric Shroom")]
    AgaricShroom,
    [Description("Shell Ointment")]
    ShellOintment,
    [Description("Crimson Ore")]
    CrimsonOre,
    [Description("Bee Hat")]
    BeeHat,
    [Description("Blank Card")]
    BlankCard,
    [Description("1-Suit Card")]
    SuitCard1,
    [Description("2-Suit Card")]
    SuitCard2,
    [Description("3-Suit Card")]
    SuitCard3,
    [Description("Full Suit Card")]
    FullSuitCard,
    [Description("Heaven Key")]
    HeavenKey,
    [Description("Earth Key")]
    EarthKey,
    [Description("Longleg Summoner")]
    LonglegSummoner,
    [Description("Stolen Silk")]
    StolenSilk,
    [Description("Mysterious Piece")]
    MysteriousPiece,
    [Description("Game Tokens")]
    GameTokens,
    [Description("Rusty Key")]
    RustyKey,
    [Description("Orange Horn")]
    OrangeHorn,
    [Description("Sand Castle Key")]
    SandCastleKey,
    [Description("Ancient Key")]
    AncientKey,
    [Description("Big Ancient Key")]
    BigAncientKey,
    [Description("Peculiar Gem")]
    PeculiarGem,
    [Description("Top Hat")]
    TopHat,
    [Description("Crystal Fruit")]
    CrystalFruit,
    [Description("Wasp Key")]
    WaspKey,
    [Description("Jayde's Stew")]
    JaydeStew,
    [Description("Dark Cherries")]
    DarkCherries,
    [Description("Cherry Pie")]
    CherryPie,
    [Description("Miracle Shake")]
    MiracleShake,
    [Description("Berry Smoothie")]
    BerrySmoothie,
    [Description("Squash")]
    Squash,
    [Description("Squash Candy")]
    SquashCandy,
    [Description("Sophie Petal")]
    SophiePetal,
    [Description("Succulent Cookies")]
    SucculentCookies,
    [Description("Sweet Pudding")]
    SweetPudding,
    [Description("Mega-Rush™")]
    MegaRush,
    [Description("HP Potion")]
    HPPotion,
    [Description("ATK Potion")]
    ATKPotion,
    [Description("DEF Potion")]
    DEFPotion,
    [Description("TP Potion")]
    TPPotion,
    [Description("Super HP Potion")]
    SuperHPPotion,
    [Description("Super TP Potion")]
    SuperTPPotion,
    [Description("MP Potion")]
    MPPotion,
    [Description("Shady Note")]
    ShadyNote,
    [Description("Blackest Paint")]
    BlackestPaint,
    [Description("Red Paint")]
    RedPaint,
    [Description("Root Cloth")]
    RootCloth,
    [Description("Ant Doll")]
    AntDoll,
    [Description("Eastern Doll")]
    EasternDoll,
    [Description("Mushroom Gummies")]
    MushroomGummies,
    [Description("Wrapped Lunch")]
    WrappedLunch,
    [Description("Crystal Crown")]
    CrystalCrown,
    [Description("Drowsy Cake")]
    DrowsyCake,
    [Description("Leaf Croissant")]
    LeafCroissant,
    [Description("Package")]
    Package,
    [Description("Crystal Feather")]
    CrystalFeather,
    [Description("Crystal Fang")]
    CrystalFang,
    [Description("Cherry Bombs")]
    CherryBombs,
    [Description("Squash Puree")]
    SquashPuree,
    [Description("Mite Burger")]
    MiteBurger,
    [Description("Aphid Shake")]
    AphidShake,
    [Description("Mystery Berry")]
    MysteryBerry,
    [Description("Small Gear")]
    SmallGear,
    [Description("Medium Gear")]
    MediumGear,
    [Description("Big Gear")]
    BigGear,
    [Description("Lab Card")]
    LabCard,
    [Description("Prison Key")]
    PrisonKey,
    [Description("Hustle Berry")]
    HustleBerry,
    [Description("History Book (Blue))")]
    HistoryBookBlue,
    [Description("History Book (Green)")]
    HistoryBookGreen,
    [Description("History Book (Yellow))")]
    HistoryBookYellow,
    [Description("History Book (Red)")]
    HistoryBookRed,
    [Description("Leaf Umbrella")]
    LeafUmbrella,
    [Description("Venom Ribbon")]
    VenomRibbon,
    [Description("Shocking Ribbon")]
    ShockingRibbon,
    [Description("Drowsy Ribbon")]
    DrowsyRibbon,
    [Description("Big Mistake")]
    BigMistake,
    [Description("Burly Bomb")]
    BurlyBomb,
    [Description("Berry Jam")]
    BerryJam,
    [Description("Bad Book")]
    BadBook,
    [Description("Mechanical Claw")]
    MechanicalClaw,
    [Description("Platinum Card")]
    PlatinumCard,
    [Description("Hot Drink")]
    HotDrink,
    [Description("Hustle Candy")]
    HustleCandy,
    [Description("Squash Tart")]
    SquashTart,
    [Description("Plumpling Pie")]
    PlumplingPie,
    [Description("Danger Dish")]
    DangerDish,
    [Description("Honey Pancakes")]
    HoneyPancakes,
    [Description("Flame Rock")]
    FlameRock,
    [Description("Card Trophy")]
    CardTrophy,
    [Description("Team Ribbon")]
    TeamRibbon,
    COUNT
  };

  public enum Song
  {
    [Description("Outskirts")]
    Outskirts = 0,
    [Description("Let's fight, team!")]
    LetsFightTeam,
    [Description("Snakemouth Den")]
    SnakemouthDen,
    [Description("They Call Him Spuder")]
    TheyCallHimSpuder,
    [Description("The Everlasting Sapling")]
    TheEverlastingSapling,
    [Description("Where Adventurers Gather")]
    WhereAdventurersGather,
    [Description("Getting Stronger!")]
    GettingStronger,
    [Description("Fine Arts")]
    FineArts,
    [Description("It's Getting Scary!")]
    ItsGettingScary,
    [Description("Let's Fry with Fry!")]
    LetsFrywithFry,
    [Description("The One Left Behind")]
    TheOneLeftBehind,
    [Description(".")]
    UNUSEDBeetle,
    [Description("Bug Fables")]
    BugFables,
    [Description("Blessed Golden Lands")]
    BlessedGoldenLands,
    [Description("Dodgy Business")]
    DodgyBusiness,
    [Description("In The Court of The Ant Queen")]
    InTheCourtOfTheAntQueen,
    [Description("The Sacred Hills")]
    TheSacredHills,
    [Description("Mothiva! SUPERSTAR!")]
    MothivaSUPERSTAR,
    [Description("Harvest Festival")]
    HarvestFestival,
    [Description("Frenzied Sunflower Dance")]
    FrenziedSunflowerDance,
    [Description("Venus, Goddess of Bountiful Harvests")]
    VenusGoddessofBountifulHarvests,
    [Description("Defiant Root")]
    DefiantRoot,
    [Description("The Bandits' Hideout")]
    TheBanditsHideout,
    [Description("Team, this one's stronger!")]
    TeamThisOnesStronger,
    [Description("Lost Sands")]
    LostSands,
    [Description("Oh no! WASPS!")]
    OhnoWASPS,
    [Description(".")]
    WindAmbient,
    [Description(".")]
    WaterAmbient,
    [Description("Work that Honey")]
    WorkThatHoney,
    [Description("Kut it Up!")]
    KutItUp,
    [Description("Make it Crisbee!")]
    MakeItCrisbee,
    [Description("Bee Kingdom, Miles Above")]
    BeeKingdomMilesAbove,
    [Description("Mecha Bee Destroyer Blastlord")]
    MechaBeeDestroyerBlastlord,
    [Description("Store that Honey")]
    StoreThatHoney,
    [Description("The Ones Who...")]
    TheOnesWho,
    [Description("Bugaria's Latest Sensation!")]
    BugariaLatestSensation,
    [Description("Bianca, Queen of All Bees")]
    BiancaQueenOfAllBees,
    [Description("Drums of War")]
    DrumsofWar,
    [Description(".")]
    MachineHumAmbient,
    [Description("The Watcher")]
    TheWatcher,
    [Description("Lost Castle of Ancient Worship")]
    LostCastleOfAncientWorship,
    [Description("Swamp Where Dreams Drown")]
    SwampWhereDreamsDrown,
    [Description("Forsaken Lands")]
    ForsakenLands,
    [Description("Team, it's getting serious!")]
    TeamItsGettingSerious,
    [Description("Caves")]
    Caves,
    [Description("Elizant II's Promise")]
    ElizantIIPromise,
    [Description("Cruel Beast, Devourer of Journeys")]
    CruelBeastDevourerOfJourneys,
    [Description("Lands Untamed")]
    LandsUntamed,
    [Description("Snug as a Bug in a Sub")]
    SnugAsABugInASub,
    [Description("Termite Capitol")]
    TermiteCapitol,
    [Description(".")]
    BreathingAmbient,
    [Description("DineMite Beats")]
    DineMiteBeats,
    [Description("Wasp Kingdom")]
    WaspKingdom,
    [Description("Rubber Prison")]
    RubberPrison,
    [Description("Ant Kingdom, Under Attack...!")]
    AntKingdomUnderAttack,
    [Description("Summer Holiday at the Metal Island™")]
    SummerHolidayattheMetalIsland,
    [Description("Reckless For Glory")]
    RecklessForGlory,
    [Description("Centipede Attacks")]
    CentipedeAttacks,
    [Description("Lab Over Snakemouth")]
    LabOverSnakemouth,
    [Description("Flower Journey")]
    FlowerJourney,
    [Description("The Other One")]
    TheOtherOne,
    [Description("Battle Against Ultimax, Who Has A Tank")]
    BattleAgainstUltimaxWhoHasATank,
    [Description("????")]
    DeadLanderGammaChase,
    [Description("The Giant's Lair")]
    TheGiantsLair,
    [Description(".")]
    UNUSEDGiant,
    [Description(".")]
    UNUSEDGiant2,
    [Description("The Usurper, Grasping for Power ")]
    TheUsurperGraspingForPower,
    [Description("Transcending, Overpowering, Everlasting")]
    TranscendingOverpoweringEverlasting,
    [Description("Mite Knight")]
    MiteKnight,
    [Description("Team... We've done it!")]
    TeamWeVeDoneIt,
    [Description("The Sailors' Pier")]
    TheSailorsPier,
    [Description("A Long Journey's End")]
    ALongJourneysEnd,
    [Description("The Usurper ")]
    TheUsurper,
    [Description("Bustling Ant Kingdom!")]
    BustlingAntKingdom,
    COUNT
  };

  public enum Discovery
  {
    [Description("Snakemouth Den")]
    SnakemouthDen = 0,
    [Description("Snakemouth Depths")]
    SnakemouthDepths,
    [Description("Explorer's Message")]
    ExplorerMessage,
    [Description("The Roaches' Village?")]
    TheRoachesVillage,
    [Description("Inn Foremothers")]
    InnForemothers,
    [Description("The Settler Statue")]
    TheSettlerStatue,
    [Description("The Ants")]
    TheAnts,
    [Description("The Bees")]
    TheBees,
    [Description("The Termites")]
    TheTermites,
    [Description("The Wasps")]
    TheWasps,
    [Description("Queen Elizant I")]
    QueenElizantI,
    [Description("The Anthill Palace")]
    TheAnthillPalace,
    [Description("Aphids and Cochinaels")]
    AphidsAndCochinaels,
    [Description("The Golden Festival")]
    TheGoldenFestival,
    [Description("The Goddess' Statue")]
    TheGoddessStatue,
    [Description("Honoured Employee")]
    HonouredEmployee,
    [Description("Sand Castle Mural")]
    SandCastleMural,
    [Description("Snakemouth's Lab")]
    SnakemouthLab,
    [Description("Healing Sophie")]
    HealingSophie,
    [Description("Seedling Haven")]
    SeedlingHaven,
    [Description("Underground Tavern")]
    UndergroundTavern,
    [Description("The Ant Mines")]
    TheAntMines,
    [Description("Chomper Cavern")]
    ChomperCavern,
    [Description("The Power Plant")]
    ThePowerPlant,
    [Description("The Bee Kingdom")]
    TheBeeKingdom,
    [Description("Balcony Telescope")]
    BalconyTelescope,
    [Description("B.O.S.S.")]
    BOSS,
    [Description("Defiant Root")]
    DefiantRoot,
    [Description("Relic Museum")]
    RelicMuseum,
    [Description("The Lost Sands")]
    TheLostSands,
    [Description("Tardigrade Idol")]
    TardigradeIdol,
    [Description("Bandit Hideout")]
    BanditHideout,
    [Description("Stream Mountain")]
    StreamMountain,
    [Description("Far Grasslands")]
    FarGrasslands,
    [Description("Fishing Village")]
    FishingVillage,
    [Description("Wizard's Tower")]
    WizardTower,
    [Description("Far Swamplands")]
    FarSwamplands,
    [Description("Wasp Kingdom")]
    WaspKingdom,
    [Description("Forsaken Lands")]
    ForsakenLands,
    [Description("Ancient City")]
    AncientCity,
    [Description("Termite Kingdom")]
    TermiteKingdom,
    [Description("Termite Statue")]
    TermiteStatue,
    [Description("Termite Coliseum")]
    TermiteColiseum,
    [Description("Termacade")]
    Termacade,
    [Description("Sailor's Statue")]
    SailorStatue,
    [Description("Metal Island")]
    MetalIsland,
    [Description("Spy Card")]
    SpyCard,
    [Description("Peacock Island")]
    PeacockIsland,
    [Description("Rubber Prison")]
    RubberPrison,
    [Description("Giant's Lair")]
    GiantLair,
    COUNT
  };

  public enum Enemy
  {
    [Description("Zombiant")]
    Zombiant = 0,
    [Description("Jellyshroom")]
    Jellyshroom,
    [Description("Spider")]
    Spider,
    [Description("Zasp")]
    Zasp,
    [Description("Cactiling")]
    Cactiling,
    [Description("Psicorp")]
    Psicorp,
    [Description("Thief")]
    Thief,
    [Description("Bandit")]
    Bandit,
    [Description("Inichas")]
    Inichas,
    [Description("Seedling")]
    Seedling,
    [Description("Flying Seedling")]
    FlyingSeedling,
    [Description("Maki (Scripted)")]
    MakiScripted,
    [Description("Web")]
    Web,
    [Description("Spider (Scripted)")]
    SpiderScripted,
    [Description("Numbnail")]
    Numbnail,
    [Description("Mothiva")]
    Mothiva,
    [Description("Acornling")]
    Acornling,
    [Description("Weevil")]
    Weevil,
    [Description("Mr. Tester")]
    MrTester,
    [Description("Venus' Bud")]
    VenusBud,
    [Description("Chomper")]
    Chomper,
    [Description("Acolyte Aria")]
    AcolyteAria,
    [Description("Vine")]
    Vine,
    [Description("Kabbu")]
    Kabbu,
    [Description("Venus' Guardian")]
    VenusGuardian,
    [Description("Wasp Trooper")]
    WaspTrooper,
    [Description("Wasp Bomber")]
    WaspBomber,
    [Description("Wasp Driller")]
    WaspDriller,
    [Description("Wasp Scout")]
    WaspScout,
    [Description("Midge")]
    Midge,
    [Description("Underling")]
    Underling,
    [Description("Monsieur Scarlet")]
    MonsieurScarlet,
    [Description("Golden Seedling")]
    GoldenSeedling,
    [Description("Arrow Worm")]
    ArrowWorm,
    [Description("Carmina")]
    Carmina,
    [Description("Seedling King")]
    SeedlingKing,
    [Description("Broodmother")]
    Broodmother,
    [Description("Plumpling")]
    Plumpling,
    [Description("Flowerling")]
    Flowerling,
    [Description("Burglar")]
    Burglar,
    [Description("Astotheles")]
    Astotheles,
    [Description("Mother Chomper")]
    MotherChomper,
    [Description("Ahoneynation")]
    Ahoneynation,
    [Description("Bee-Boop")]
    BeeBoop,
    [Description("Security Turret")]
    SecurityTurret,
    [Description("Denmuki")]
    Denmuki,
    [Description("Heavy Drone B-33")]
    HeavyDroneB33,
    [Description("Mender")]
    Mender,
    [Description("Abomihoney")]
    Abomihoney,
    [Description("Dune Scorpion")]
    DuneScorpion,
    [Description("Tidal Wyrm")]
    TidalWyrm,
    [Description("Kali")]
    Kali,
    [Description("Zombee")]
    Zombee,
    [Description("Zombeetle")]
    Zombeetle,
    [Description("The Watcher")]
    TheWatcher,
    [Description("Peacock Spider")]
    PeacockSpider,
    [Description("Bloatshroom")]
    Bloatshroom,
    [Description("Krawler")]
    Krawler,
    [Description("Haunted Cloth")]
    HauntedCloth,
    [Description("Sand Wall")]
    SandWall,
    [Description("Ice Wall")]
    IceWall,
    [Description("Warden")]
    Warden,
    [Description("Wasp King (Scripted)")]
    WaspKingScripted,
    [Description("Jumping Spider")]
    JumpingSpider,
    [Description("Mimic Spider")]
    MimicSpider,
    [Description("Leafbug Ninja")]
    LeafbugNinja,
    [Description("Leafbug Archer")]
    LeafbugArcher,
    [Description("Leafbug Clubber")]
    LeafbugClubber,
    [Description("Madesphy")]
    Madesphy,
    [Description("The Beast")]
    TheBeast,
    [Description("Chomper Brute")]
    ChomperBrute,
    [Description("Mantidfly")]
    Mantidfly,
    [Description("General Ultimax")]
    GeneralUltimax,
    [Description("Wild Chomper")]
    WildChomper,
    [Description("Cross")]
    Cross,
    [Description("Poi")]
    Poi,
    [Description("Primal Weevil")]
    PrimalWeevil,
    [Description("False Monarch")]
    FalseMonarch,
    [Description("Mothfly")]
    Mothfly,
    [Description("Mothfly Cluster")]
    MothflyCluster,
    [Description("Ironnail")]
    Ironnail,
    [Description("Belostoss")]
    Belostoss,
    [Description("Ruffian")]
    Ruffian,
    [Description("Water Strider")]
    WaterStrider,
    [Description("Diving Spider")]
    DivingSpider,
    [Description("Cenn")]
    Cenn,
    [Description("Pisci")]
    Pisci,
    [Description("Dead Lander α")]
    DeadLanderAlpha,
    [Description("Dead Lander β")]
    DeadLanderBeta,
    [Description("Dead Lander γ")]
    DeadLanderGamma,
    [Description("Wasp King")]
    WaspKing,
    [Description("The Everlasting King")]
    TheEverlastingKing,
    [Description("Maki")]
    Maki,
    [Description("Kina")]
    Kina,
    [Description("Yin")]
    Yin,
    [Description("ULTIMAX Tank")]
    ULTIMAXTank,
    [Description("Zommoth")]
    Zommoth,
    [Description("Riz")]
    Riz,
    [Description("Devourer")]
    Devourer,
    [Description("Tail")]
    Tail,
    [Description("Rock Wall")]
    RockWall,
    [Description("Ancient Key")]
    AncientKey1,
    [Description("Ancient Key")]
    AncientKey2,
    [Description("Ancient Tablet")]
    AncientTablet,
    [Description("Flytrap")]
    Flytrap,
    [Description("FireKrawler")]
    FireKrawler,
    [Description("FireWarden")]
    FireWarden,
    [Description("FireCape")]
    FireCape,
    [Description("IceKrawler")]
    IceKrawler,
    [Description("IceWarden")]
    IceWarden,
    [Description("TANGYBUG")]
    Tangybug,
    [Description("Stratos")]
    Stratos,
    [Description("Delilah")]
    Delilah,
    COUNT
  };

  public enum Recipe
  {
    [Description("Mistake")]
    Mistake = 0,
    [Description("Leaf Salad")]
    LeafSalad,
    [Description("Glazed Honey")]
    GlazedHoney,
    [Description("Abomihoney")]
    Abomihoney,
    [Description("Cooked Shroom")]
    CookedShroom,
    [Description("Cooked Danger")]
    CookedDanger,
    [Description("Sweet Shroom")]
    SweetShroom,
    [Description("Sweet Danger")]
    SweetDanger,
    [Description("Fried Egg")]
    FriedEgg,
    [Description("Leaf Omelet")]
    LeafOmelet,
    [Description("Honey'd Leaf")]
    HoneydLeaf,
    [Description("Roasted Berries")]
    RoastedBerries,
    [Description("Succulent Platter")]
    SucculentPlatter,
    [Description("Succulent Cookies")]
    SucculentCookies,
    [Description("Berry Juice")]
    BerryJuice,
    [Description("Mushroom Skewer")]
    MushroomSkewer,
    [Description("Hearty Breakfast")]
    HeartyBreakfast,
    [Description("Mushroom Salad")]
    MushroomSalad,
    [Description("Sweet Dew")]
    SweetDew,
    [Description("Baked Yam")]
    BakedYam,
    [Description("Spicy Fries")]
    SpicyFries,
    [Description("Burly Chips")]
    BurlyChips,
    [Description("Danger Dish")]
    DangerDish,
    [Description("Shaved Ice")]
    ShavedIce,
    [Description("Cold Salad")]
    ColdSalad,
    [Description("Ice Cream")]
    IceCream,
    [Description("Honey Ice Cream")]
    HoneyIceCream,
    [Description("Dry Bread")]
    DryBread,
    [Description("Nutty Cake")]
    NuttyCake,
    [Description("Poison Cake")]
    PoisonCake,
    [Description("Frost Pie")]
    FrostPie,
    [Description("Yam Bread")]
    YamBread,
    [Description("Shock Candy")]
    ShockCandy,
    [Description("Mushroom Gummies")]
    MushroomGummies,
    [Description("Sweet Pudding")]
    SweetPudding,
    [Description("Leaf Croissant")]
    LeafCroissant,
    [Description("Honey Pancakes")]
    HoneyPancakes,
    [Description("Drowsy Cake")]
    DrowsyCake,
    [Description("Hustle Candy")]
    HustleCandy,
    [Description("Spicy Candy")]
    SpicyCandy,
    [Description("Burly Candy")]
    BurlyCandy,
    [Description("Berry Jam")]
    BerryJam,
    [Description("Hot Drink")]
    HotDrink,
    [Description("Plain Tea")]
    PlainTea,
    [Description("Spicy Tea")]
    SpicyTea,
    [Description("Burly Tea")]
    BurlyTea,
    [Description("Aphid Shake")]
    AphidShake,
    [Description("Squash Puree")]
    SquashPuree,
    [Description("Squash Candy")]
    SquashCandy,
    [Description("Squash Tart")]
    SquashTart,
    [Description("Plumpling Pie")]
    PlumplingPie,
    [Description("Tangy Jam")]
    TangyJam,
    [Description("Tangy Juice")]
    TangyJuice,
    [Description("Tangy Pie")]
    TangyPie,
    [Description("Spicy Bomb")]
    SpicyBomb,
    [Description("Burly Bomb")]
    BurlyBomb,
    [Description("Poison Bomb")]
    PoisonBomb,
    [Description("Sleep Bomb")]
    SleepBomb,
    [Description("Numb Bomb")]
    NumbBomb,
    [Description("Frost Bomb")]
    FrostBomb,
    [Description("Clear Bomb")]
    ClearBomb,
    [Description("Abombination")]
    Abombination,
    [Description("Cherry Bombs")]
    CherryBombs,
    [Description("Cherry Pie")]
    CherryPie,
    [Description("Berry Smoothie")]
    BerrySmoothie,
    [Description("Miracle Shake")]
    MiracleShake,
    [Description("Tangy Carpaccio")]
    TangyCarpaccio,
    [Description("Crisbee Doughnut")]
    CrisbeeDoughnut,
    [Description("Queen's Dinner")]
    QueenDinner,
    [Description("Big Mistake")]
    BigMistake,
    COUNT
  };

  public enum Record
  {
    [Description("Ultimate Team!")]
    UltimateTeam = 0,
    [Description("Crystal Collector")]
    CrystalCollector,
    [Description("Medal Collector")]
    MedalCollector,
    [Description("Music Collector")]
    MusicCollector,
    [Description("Battle Ready")]
    BattleReady,
    [Description("Chapter 1 Complete")]
    Chapter1Complete,
    [Description("Vicious Spider")]
    ViciousSpider,
    [Description("Helping Hand")]
    HelpingHand,
    [Description("Cooking Maestro")]
    CookingMaestro,
    [Description("Field Researcher")]
    FieldResearcher,
    [Description("Pro Explorers")]
    ProExplorers,
    [Description("Good Deed")]
    GoodDeed,
    [Description("Chapter 2 Complete")]
    Chapter2Complete,
    [Description("The Guardian")]
    TheGuardian,
    [Description("Chapter 3 Complete")]
    Chapter3Complete,
    [Description("Heavy Duty")]
    HeavyDuty,
    [Description("Chapter 4 Complete")]
    Chapter4Complete,
    [Description("Always Watchful")]
    AlwaysWatchful,
    [Description("Chapter 5 Complete")]
    Chapter5Complete,
    [Description("The Terror")]
    TheTerror,
    [Description("Chapter 6 Complete")]
    Chapter6Complete,
    [Description("All Geared Up")]
    AllGearedUp,
    [Description("Chapter 7 Complete")]
    Chapter7Complete,
    [Description("The End")]
    TheEnd,
    [Description("Plant Enchanter")]
    PlantEnchanter,
    [Description("Reconciliation")]
    Reconcillation,
    [Description("The Truth")]
    TheTruth,
    [Description("Our Job's Done!")]
    OurJobsDone,
    [Description("A Better Bugaria")]
    ABetterBugaria,
    [Description("Gamer")]
    Gamer,
    COUNT
  };

  public enum Area
  {
    [Description("Bugaria Outskirts")]
    BugariaOutskirts = 0,
    [Description("Ant Kingdom City")]
    AntKingdomCity,
    [Description("Snakemouth Den")]
    SnakemouthDen,
    [Description("Lost Sands")]
    LostSands,
    [Description("Golden Hills")]
    GoldenHills,
    [Description("Golden Path")]
    GoldenPath,
    [Description("Golden Settlement")]
    GoldenSettlement,
    [Description("Forsaken Lands")]
    ForsakenLands,
    [Description("Far Grasslands")]
    FarGrasslands,
    [Description("Wild Swamplands")]
    WildSwamplands,
    [Description("Defiant Root")]
    DefiantRoot,
    [Description("Ancient Castle")]
    AncientCastle,
    [Description("Bee Kingdom Hive")]
    BeeKingdomHive,
    [Description("Honey Factory")]
    HoneyFactory,
    [Description("Rubber Prison")]
    RubberPrison,
    [Description("Giant's Lair")]
    GiantsLair,
    [Description("Metal Lake")]
    MetalLake,
    [Description("Metal Island")]
    MetalIsland,
    [Description("Termite Capitol")]
    TermiteCapitol,
    [Description("Wasp Kingdom Hive")]
    WaspKingdomHive,
    [Description("Bandit Hideout")]
    BanditHideout,
    [Description("Stream Mountain")]
    StreamMountain,
    [Description("Chomper Caves")]
    ChomperCaves,
    [Description("Fishing Village")]
    FishingVillage,
    [Description("Upper Snakemouth")]
    UpperSnakemouth,
    COUNT
  };

  public enum SpyCard
  {
    [Description("Zombiant")]
    Zombiant,
    [Description("Jellyshroom")]
    Jellyshroom,
    [Description("Spider")]
    Spider,
    [Description("Venus' Guardian")]
    VenusGuardian,
    [Description("Zasp (Chp. 2)")]
    ZaspCh2,
    [Description("Mothiva (Chp. 2)")]
    MothivaCh2,
    [Description("Acolyte Aria")]
    AcolyteAria,
    [Description("Monsieur Scarlet")]
    MonsieurScarlet,
    [Description("Inichas")]
    Inichas,
    [Description("Cactiling")]
    Cactiling,
    [Description("Psicorp")]
    Psicorp,
    [Description("Thief")]
    Thief,
    [Description("Bandit")]
    Bandit,
    [Description("Seedling")]
    Seedling,
    [Description("Numbnail")]
    Numbnail,
    [Description("Acornling")]
    Acornling,
    [Description("Weevil")]
    Weevil,
    [Description("Venus' Bud")]
    VenusBud,
    [Description("Chomper")]
    Chomper,
    [Description("Kabbu")]
    Kabbu,
    [Description("Wasp Trooper")]
    WaspTrooper,
    [Description("Midge")]
    Midge,
    [Description("Underling")]
    Underling,
    [Description("Golden Seedling")]
    GoldenSeedling,
    [Description("Burglar")]
    Burglar,
    [Description("Bee-Boop")]
    BeeBoop,
    [Description("Mother Chomper")]
    MotherChomper,
    [Description("Abomihoney")]
    Abomihoney,
    [Description("Carmina")]
    Carmina,
    [Description("Security Turret")]
    SecurityTurret,
    [Description("Denmuki")]
    Denmuki,
    [Description("Wasp Scout")]
    WaspScout,
    [Description("Arrow Worm")]
    ArrowWorm,
    [Description("Ahoneynation")]
    Ahoneynation,
    [Description("Mender")]
    Mender,
    [Description("Heavy Drone B-33")]
    HeavyDroneB33,
    [Description("Broodmother")]
    Broodmother,
    [Description("Astotheles")]
    Astotheles,
    [Description("Dune Scorpion")]
    DuneScorpion,
    [Description("Tidal Wyrm")]
    TidalWyrm,
    [Description("Seedling King")]
    SeedlingKing,
    [Description("Kali")]
    Kali,
    [Description("Zombee")]
    Zombee,
    [Description("Zombeetle")]
    Zombeetle,
    [Description("The Watcher")]
    TheWatcher,
    [Description("Peacock Spider")]
    PeacockSpider,
    [Description("Bloatshroom")]
    Bloatshroom,
    [Description("Krawler")]
    Krawler,
    [Description("Haunted Cloth")]
    HauntedCloth,
    [Description("Warden")]
    Warden,
    [Description("Wasp King (Chp. 7)")]
    WaspKingCh7,
    [Description("Jumping Spider")]
    JumpingSpider,
    [Description("Mimic Spider")]
    MimicSpider,
    [Description("Leafbug Ninja")]
    LeafbugNinja,
    [Description("Leafbug Archer")]
    LeafbugArcher,
    [Description("Leafbug Clubber")]
    LeafbugClubber,
    [Description("Madesphy")]
    Madesphy,
    [Description("The Beast")]
    TheBeast,
    [Description("Chomper Brute")]
    ChomperBrute,
    [Description("Mantidfly")]
    Mantidfly,
    [Description("General Ultimax")]
    GeneralUltimax,
    [Description("ULTIMAX Tank")]
    ULTIMAXTank,
    [Description("Wild Chomper")]
    WildChomper,
    [Description("Cross")]
    Cross,
    [Description("Poi")]
    Poi,
    [Description("Primal Weevil")]
    PrimalWeevil,
    [Description("False Monarch")]
    FalseMonarch,
    [Description("Mothfly")]
    Mothfly,
    [Description("Mothfly Cluster")]
    MothflyCluster,
    [Description("Ironnail")]
    Ironnail,
    [Description("Belostoss")]
    Belostoss,
    [Description("Ruffian")]
    Ruffian,
    [Description("Water Strider")]
    WaterStrider,
    [Description("Diving Spider")]
    DivingSpider,
    [Description("Cenn")]
    Cenn,
    [Description("Pisci")]
    Pisci,
    [Description("Dead Lander α")]
    DeadLanderα,
    [Description("Dead Lander β")]
    DeadLanderβ,
    [Description("Dead Lander γ")]
    DeadLanderγ,
    [Description("The Everlasting King")]
    TheEverlastingKing,
    [Description("Maki")]
    Maki,
    [Description("Kina")]
    Kina,
    [Description("Yin")]
    Yin,
    [Description("Zommoth")]
    Zommoth,
    [Description("Riz")]
    Riz,
    [Description("Devourer")]
    Devourer,
    [Description("Wasp Bomber")]
    WaspBomber,
    [Description("Wasp Driller")]
    WaspDriller,
    [Description("Plumpling")]
    Plumpling,
    [Description("Flowerling")]
    Flowerling,
    [Description("Stratos")]
    Stratos,
    [Description("Delilah")]
    Delilah,
    COUNT
  };

  public enum Map
  {
    TestRoom = 0,
    NearSnakemouth,
    OutsideSnakemouth,
    AntTunnels,
    DesertEntrance,
    DesertBadlands,
    DesertBookArea,
    DesertRockFormation,
    DesertTrenchSouth,
    BugariaMainPlaza,
    BugariaCommercial,
    SnakemouthBridgeRoom,
    SnakemouthDoorRoom,
    SnakemouthFallRoom,
    SnakemouthLake,
    SnakemouthEmpty,
    BugariaOutskirtsOutsideCity,
    BugariaOutskitsSnakemouthCorridor1,
    BugariaOutskirtsSnakemouthCorridor2,
    SnakemouthUndergrondDoor,
    SnakemouthMushroomPit,
    SnakemouthTreasureRoom,
    SnakemouthUndergroundRightA,
    SnakemouthUndergroundRightB,
    SnakemouthUndergroundLeftA,
    SnakemouthUndergroundLeftB,
    BugariaTheater,
    ChucksAbode,
    BugariaResidential,
    GoldenHillsCableCar,
    UndergroundBar,
    AntPalace1,
    AntPalace2,
    AntBridge,
    AntPalaceLibrary,
    GoldenPathTunnel,
    BOGoldenPath,
    AntPalaceWarRoom,
    GoldenHillsPath2,
    GoldenSettlementEntrance,
    GoldenSettlement1,
    GoldenSettlement1Night,
    GoldenSettlement2,
    GoldenSettlement2Night,
    GoldenHillsPath3,
    GoldenHillsDungeonEntrance,
    GoldenHillsDungeonLeftMain,
    GoldenHillsDungeonCrankLeft,
    GoldenHillsDungeonRightCrank,
    GoldenHillsLowerRightCrank,
    GoldenHillsDungeonLeftCrankHalf,
    GoldenHillsDungeonUpperMain,
    GoldenHillsDungeonUpperSide,
    GoldenHillsDungeonBoss,
    BugariaPier,
    BugariaOutskirtsEast1,
    BugariaOutskirtsEast2,
    BOLostSandsEntrance,
    DefiantRoot1,
    DefiantRootWell,
    DefiantRoot2,
    DefiantRoot3,
    BeehiveOutside,
    BeehiveThroneRoom,
    BeehiveScannerRoom,
    GoldenSettlement3,
    GoldenSettlement3Night,
    BeehiveMainArea,
    HBsLab,
    BeehiveBalcony,
    HoneycombsLab,
    JaunesGallery,
    HoneyFactoryEntrance,
    AntMinesBreakRoom,
    HoneyFactoryWorkerRooms,
    HoneyFactoryCore,
    DesertDREastEntrance,
    DesertFGBorder,
    DesertDRSouthEntrance,
    DesertBadgeAlcove,
    DesertCaravanMap,
    DesertSandPitArea,
    DesertBeforeGH,
    FactoryProcessingFirstRoom,
    FactoryProcessing2,
    FactoryProcessingPump,
    FactoryProcessingPuzzle1,
    FactoryProcessingPuzzle2,
    FactoryProcessingPuzzle3,
    FactoryProcessingMalbee,
    FactoryStorageMaze,
    FactoryStorageElevator,
    FactoryStorageMiniboss,
    FactoryStorageOverseer,
    MetalIsland1,
    DesertRoachVillage,
    DesertOasis,
    DesertOasisEntrance,
    DesertWestDunes,
    HideoutEntrance,
    HideoutCell,
    HideoutCentralRoom,
    HideoutLeftA,
    HideoutStairsRoom,
    HideoutGarden,
    HideoutWestStorage,
    HideoutRightA,
    DesertSandCastle,
    DesertMountain,
    DesertTrenchMiddle,
    DesertJumpPuzzle,
    DesertSouthern,
    DesertScorpion,
    DesertEastmost,
    GoldenSMinigame,
    Blank,
    SandCastleEntrance,
    SandCastleSlidePuzzle,
    SandCastleStatueRoom,
    SandCastleBasement,
    SandCastleRoof,
    SandCastleMainRoom,
    SandCastleBossKeyRoom,
    BugariaPlazaAttack,
    BugariaBridgeAttack,
    BugariaCastleAttack,
    SandCastlePressurePuzzle,
    SandCastleRockRoom,
    SandCastleBossRoom,
    SandCastleTreasureRoom,
    BugariaAssociationAttack,
    MetalIsland2,
    StreamMountain1,
    StreamMountain2,
    StreamMountain3,
    FGCave,
    SeedlingHaven,
    FarGrasslands1,
    FarGrasslandsOutsideCave,
    FarGrasslandsWizard,
    FarGrasslands2,
    FarGrasslandsLake,
    FarGrasslandsOutsideVillage,
    FarGrasslands3,
    FishingVillage,
    SwamplandsEntrance,
    FGOutsideSwamplands,
    WaspKingdomOutside,
    Swamplands2,
    BarrenLandsEntrance,
    BarrenLandsCD,
    Swamplands3,
    SwamplandsBridge,
    FarGrasslands4,
    SwamplandsBoss,
    ChomperCave1,
    ChomperCaves2,
    ChomperCaves3,
    Swamplands4,
    Swamplands5,
    Swamplands6,
    Swamplands7,
    Swamplands8,
    WaspKingdom1,
    WaspKingdom2,
    WaspKingdom3,
    WaspKingdom4,
    WaspKingdom5,
    WaspKingdomPrison,
    WaspKingdomJayde,
    WaspKingdomMainHall,
    WaspKingdomThrone,
    WaspKingdomQueen,
    TermiteOutside,
    TermiteMainPlaza,
    TermiteRoyalChamber,
    TermiteIndustrial,
    TermitePier,
    TermiteColiseum1,
    TermiteColiseum2,
    BarrenLandsBeefly,
    BarrenLandsAntTunnel,
    BarrenLandsMiniboss,
    MetalLake,
    SnakemouthTop,
    CaveOfTrials,
    WizardTowerBasement,
    WizardTowerStairs,
    WizardTowerAttic,
    BarrenLandsPinkSpider,
    BarrenLandsTanks,
    BarrenLandsMushrooms,
    AbandonedCity,
    BarrenLandsPumpkins,
    BarrenLandsCloud,
    BarrenLandsRock,
    AbandonedCityTent,
    PowerPlant,
    BroodmotherLair,
    BarrenLandsSideGPT,
    GoldenPathTunnel2,
    FGClearing,
    StreamMountain4,
    GoldenPitcher1,
    StreamMountain5,
    GoldenPitcher2,
    MysteryIsland,
    MysteryIslandInside,
    UpperSnekEntrance,
    UpperSnekTransition,
    UpperSnekSwitchPuzzle,
    UpperSnekBeforeBoss,
    UpperSnekPressurePlateRoom,
    UpperSnekBossRoom,
    UpperSnekMiddleRoom,
    UpperSnekPlatformRoom,
    UpperSnekRiverPuzzle,
    UpperSnekGeizerRoom,
    RubberPrisonPier,
    RubberPrisonCheckpointCorridor,
    RubberPrisonSpikeRoom,
    RubberPrisonCells1,
    RubberPrisonCells2,
    RubberPrisonLibrary,
    RubberPrisonCafeteria,
    RubberPrisonGym,
    RubberPrisonSecurity,
    HermitCave,
    MetalIslandAuditorium,
    RubberPrisonOffice,
    RubberPrisonThirdFloor,
    RubberPrisonGiantLairBridge,
    GiantLairEntrance,
    GiantLairDeadLands1,
    GiantLairDeadLands2,
    GiantLairFridgeOutside,
    GiantLairFridgeInside,
    GiantLairRoachVillage,
    GiantLairSaplingPlains,
    PitcherPlantArena,
    BugariaEndPlaza,
    BugariaEndBridge,
    BugariaEndThrone,
    WaspKingdomDrillRoom,
    GiantLairBeforeBoss,
    GiantLairBeforeBoss2,
    COUNT
  };

  public enum AnimID
  {
    [Description("Vi")]
    Bee = 0,
    [Description("Kabbu")]
    Beetle,
    [Description("Leif")]
    Moth,
    LadybugKnight,
    ButterflyGirl,
    MessengerAnt,
    MinerAnt1,
    SleepyMinerAnt,
    OverseerMinerAnt,
    BeeMinerAnt,
    Mothiva,
    FuzzyMoth,
    MaskedMoth,
    CordycepsAnt,
    Mushroom,
    TestCube,
    TestButton,
    OldBoringBeetle,
    FlyChef,
    Jaune,
    Zasp,
    DrNeolith,
    Cactus,
    TestSign,
    SavePoint,
    DigMound,
    AntInnkeeper,
    AntKid,
    MothKid,
    Pillbug,
    [Description("Eetl")]
    OGBeetle,
    GenericAnt,
    AntSoldier1,
    Samira,
    BadgeBeetle,
    BounceShroom,
    SwitchCrystal,
    SodaCap,
    Armorpillar,
    AncientPressurePlate,
    PushRock,
    CoilyVine,
    Spuder,
    CrystalBerry,
    Seedling,
    Kina,
    Maki,
    Gen,
    Eri,
    ShielderAnt,
    TrappedMoth,
    MantisAccountant,
    AncientPlatform,
    LongAncientPlatform,
    BigCrystalSwitch,
    SmallAncientPlatform,
    Chubee,
    Thief,
    Bandit,
    SneilEnemy,
    Crickerly,
    CaravanSmolBug,
    LadybugGirl,
    LadybugBoy,
    Mar,
    Genow,
    Trist,
    OrangeBeetle,
    Acornling,
    Weevil,
    FlyTrapPlatform,
    CommonSticcBug,
    KungFuMantis,
    MrTester,
    AngryPlant,
    OmaBug,
    Madeleine,
    FlyTrap,
    MantisAcolyte,
    CurledVineGround,
    Venus,
    Bae,
    Barkeeper,
    EdgeBeetle,
    OrangeBarBug,
    Shades,
    RoyalGuard,
    MosquitoGal,
    StickShopkeeper,
    BeetleInnkeeper,
    ShyBee,
    ArrogantBee,
    BeeKid,
    ContestBee,
    AntSoldier2,
    AntCapitain,
    AntQueen,
    Libraryant1,
    Libraryant2,
    WaspTrooper,
    ScrewSwitch,
    Aphid,
    WoolyAphid,
    Cochinael,
    FatMinerAnt,
    WoodenSwitch,
    WoodenPlatform,
    MantisBarkeeper,
    SticcGirl,
    LadybugGHRed,
    LadybugGHOrange,
    SleepyStickBug,
    MantisChef,
    FarmerAnt1,
    FarmerAnt2,
    FarmerAnt3,
    BeeGuard,
    ProfHoneycomb,
    Hawk,
    Mayor,
    Midge,
    VenusGuardian,
    OldAnt,
    GenericAnt2,
    ButterflyGuy,
    FortuneTeller,
    Stratos,
    Delilah,
    BlackStickBug,
    OfferingAltar,
    Underling,
    Scarlet,
    Tanjerin,
    TanjerinHorn,
    GoldenSeedling,
    Pseudoscorp,
    Sandworm,
    Carmina,
    Burglar,
    MotherChomper,
    Sirfy,
    Isau,
    CricketShopkeeper,
    FakeLegStickBug,
    DragonflyGuy,
    CricketGuy1,
    CricketGuy2,
    FortuneSister,
    BeetleMerchantBag,
    MothMerchantBag,
    ButterflyGuyDR,
    StickBugDR,
    TermiteGirl,
    WaspInnkeeper,
    MuseumMoth,
    BumbleBarkeeper,
    CoolMosquito,
    RoachShaman,
    QueenBee,
    DocHB,
    HBAssistant,
    FashionBee,
    ToughBee,
    SmugBeeKid,
    TolBee,
    EdgeArtBee,
    FashionMoth,
    GuideBee,
    GiftShopBee,
    ChompyChan,
    BeeOverseer,
    WorkerBee1,
    WorkerBee2,
    Maldibee,
    GlassesMantis,
    BeeBot,
    SmallMothGuy,
    Abomihoney,
    Turret,
    Denmuki,
    BeeBoss,
    WaspScout,
    BigSailorGuy,
    SmallSailorGuy,
    SailorGirl,
    PierMantisCook,
    FatScubaAnt,
    ThinScubaAnt,
    Ahoneynation,
    Menderbot,
    ElectroPlatform,
    HoneyGrate,
    MadeleineButler,
    BLANK,
    NerdyCicada,
    Astotheles,
    Eophi,
    SmallMinerAnt,
    FarmerMinerAnt,
    ButterflyCMaster,
    ArcadeTermite,
    Scorpion,
    BankerAnt,
    Bulkbee,
    MantisMerchant,
    Krawler,
    Cape,
    CursedSkull,
    IcePillarObj,
    OldMoth,
    PinkMoth,
    Watcher,
    icepillar,
    SandPillar,
    WaspKing,
    Eremi,
    RollingRock,
    Abombhoney,
    WormBeetle,
    SnailBeetle,
    Kali,
    Bomby,
    DragonflyLady,
    CardGuard,
    HotelRecep,
    MIMosquito,
    Alex,
    Butomo,
    HaughtyAnt,
    SeedlingKing,
    Yin,
    Flowering,
    Plumpling,
    JumpingSpider,
    LeafbugNinja,
    LeafbugArcher,
    LeafbugClubber,
    Patton,
    MimicSpider,
    SkullCaterpillar,
    LongLegs,
    Centipede,
    Lilypad,
    ChomperBrute,
    WoodenPPlate,
    RopePlatform,
    Mantidfly,
    WaspDriller,
    WaspBomber,
    Jayde,
    WaspGeneral,
    WaspQueen,
    Futes,
    HungryAnt,
    TraitorWasp,
    WildChomper,
    Submarine,
    TermiteScientist,
    GazingTermite,
    EdgeTermite,
    SwordTermite,
    TermiteInnkeeper,
    TermiteSoldier,
    TermiteNasute,
    Zombee,
    Zombeetle,
    Bloatshroom,
    TermiteShopkeeper,
    TermiteBarkeeper,
    TermiteQuestgiver,
    TermiteKing,
    TermiteQueen,
    ButterflyGirl2,
    TiredLadybug,
    StickBug2,
    WeirdTermite,
    ShortTermite,
    ShortTermite2,
    TermiteGirl2,
    BandanaTermite,
    CherryMerchant,
    WorkingTermite,
    PierTermite,
    PoorTermite,
    PoorTermiteSister,
    ScarfTermite,
    TermiteCashier,
    TrialRoach,
    ScientistRoach,
    PrimalWeevil,
    TangySeller,
    ToyMerchant,
    Wizard,
    ColiseumTermite,
    RoyalTermiteWorker,
    SittingAnt,
    TermiteOwner,
    TermiteScientist2,
    Gachapon,
    MaskedMF1,
    MaskedMF2,
    MaskedMF3,
    MaskedMF4,
    FalseMonarch,
    Mothfly,
    MothflyCluster,
    Ironclad,
    ToeBiter,
    MidgeBroodmother,
    FLMinerAnt,
    FGMinerAnt,
    RPMinerAnt,
    CloakAnt,
    CicadaGuy2,
    StickbugGuy2,
    VeilBee,
    BumblebeeGirl,
    SmallBeetleGuy,
    LeafAnt,
    BookAnt,
    SmugWeakGuy,
    Layna,
    LaynaPet,
    BrotherTermite,
    SeedlingTermite,
    JojoTermite,
    WrappedTermite,
    Strider,
    DivingSpider,
    Cenn,
    Pisci,
    Ruffian,
    SandWyrm,
    SandWyrmTail,
    StagBeetle,
    PisciWall,
    Riz,
    RizSister,
    RizGrandpa,
    BackerStickBug,
    Zommoth,
    FitTermite,
    TermiteNasuteSmol,
    PeacockSpider,
    PrisonGate,
    PrisonGateLocal,
    SteelSwitch,
    MoleCricketGuy,
    MoleCricketGirl,
    UltimaxTank,
    Kenny,
    DeadLanderA,
    DeadLanderB,
    DeadLanderC,
    RoachElder,
    BuffRoachGuy,
    WalkingRoachGuy,
    BeeButler,
    BigBeetle,
    CowboyStickbug,
    RichAnt,
    RichKid,
    ShadyLadybug,
    RichMoth,
    TeaMoth,
    MaskStickbug,
    WindUp,
    PierGirl,
    KeyR,
    KeyL,
    Tablet,
    EverlastingKing,
    YinMoth,
    Pitcher,
    PitcherSummon,
    Poppy,
    BookWaspGuy,
    CardBumblebee,
    Effo,
    DragonflyBlacksmith,
    WaspTwinA,
    WaspTwinB,
    WaspBoyfriend,
    BombMaster,
    BombFanatic,
    OldSailorMantis,
    CardWasp,
    CardBumble,
    CardBee,
    CardJudge,
    Comfy,
    Soto,
    RoachGirl,
    RichMantis,
    RichStagBeetle,
    RichRhinoBeetle,
    CardStickbug,
    MasterSlice,
    BounceShroom2,
    FireKrawler,
    FireCape,
    FireWarden,
    TermiteGirl3,
    Cerise,
    WoodenPPlate2,
    RecupeGuy,
    Roy,
    COUNT
  };

  public enum SaveProgressIcon
  {
    [Description("No Icons")]
    NoIcons = 0,
    [Description("Ancient Mask")]
    AncientMask,
    [Description("Ancient Tablet")]
    AncientTablet,
    [Description("Ancient Key")]
    AncientKey,
    [Description("Ancient Half")]
    AncientHalf,
    [Description("Elizant Mask")]
    Elizant,
    [Description("Flame Brooch")]
    FlameBrooch,
    [Description("Wasp King's Crown")]
    WaspKingCrown,
    COUNT
  };

  public enum MedalShop
  {
    Merab = 0,
    Shades,
    COUNT
  };

  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed,
    COUNT
  };

  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored,
    COUNT
  };

  public enum MedalEquipTarget
  {
    Unequipped = -2,
    Party,
    Vi,
    Kabbu,
    Leif,
    COUNT
  };

  public enum StatBonusType
  {
    HP = 0,
    Attack,
    Defense,
    TP,
    MP,
    COUNT
  };

  public enum StatBonusTarget
  {
    Party = -1,
    Vi,
    Kabbu,
    Leif,
    COUNT
  };

  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenMapLocation,
    COUNT
  };

  public enum PrizeMedalState
  {
    [Description("Unavailable")]
    Unavalable = 0,
    [Description("Available from Artis")]
    AvailableArtis,
    [Description("Available from the Caravan")]
    AvailableCaravan,
    [Description("Obtained")]
    Obtained,
    COUNT
  };

  public enum IWannaGetBetterProgress
  {
    [Description("No Progress")]
    NoProgress = 0,
    [Description("Cooked the Yam Bread")]
    CookedYamBread,
    [Description("Cooked the Succulent Platter")]
    CookedSucculentPlatter,
    [Description("Cooked the Abomination")]
    CookedAbomination,
    [Description("Won battle against Abomihoneys")]
    WonBattleAbomihoneys,
    COUNT
  };
}
