# Better-Enemies
Various Enemy and AI adjustments aimed at making tactical combat more challenging and tactical, made by Phoenix Rising Mod Team 

# Features
- Various Improvements of Enemy AI and abilities to make enemies act "smarter"
- Various Adjustments of Enemy perks and stats to make many of them more difficult to fight against
- Adjusted base Perception value of all humans as well as equipment modifying perception, to make detection feel slightly more realistic (no more 60 tiles spotting through tiny hole in the wall)

Full Changelog here: https://docs.google.com/spreadsheets/d/1gCB-EpteJEuEnSTggOaF49PVPYjxTwc0cuZHbBUIIy4/

**AI Changes**

1. **Humans Obsessed with healing themself**	_Adjusted AI so they will only heal when have nothing else to do_
2. **Tritons with Bloodsucking arms use them over shooting weapons**	Adjusted AI so they will prioritise shooting instead
3. **Chirons with Stomper or Melee Legs leaving map when they run out of worms**	Adjusted AI to make this behaviour less likely (need testing)
4. Enemies with Dash and Melee Weapon (Better Classes Berserker Enemy, Vanilla: The Pure Assault with Vengeance Torso)	Adjusted AI to use Dash and follow it up with Melee Attack when possible
5. **Skylla recieving major amount of Viral damage will always use recover**	Adjusted AI to prevent this behaviour
6. **Skylla**	Skylla AI is adjusted to work with changed Blaster Cannon and Caterpillar Tracks (stomping effect)
7. **Enemies with Quick Aim not using this skill**	Adjusted AI to use Quick Aim frequently (doesnt seem to be working at the moment)
8. **Aspida** 	Fixed a bug with AI where it could not use Apollo LT and Themis NT weapons (if Kaos Engines was activated)
9. **Armadillo**	Fixed a bug with AI where it could not use Mephistopheles FT weapon (if Kaos Engines was activated)
10. **AI time to think**	Given an option in config (default: off) to double the time AI can think, increasing turn length but slightly improving decision making
	
**General Changes**

Pandoran Evolution	Add Arthon Gunner, Triton Wretch and Triton Footman to starting enemies. Removed Triton Strangler. (warning, this may lead to slightly faster than usual enemy evolutions)
Human Enemies	Human Enemies will deploy minimum of lvl 3 Troops, this also affect Haven Recruits and Defenders. Deployment points for lvl3 enemies decreased.
Forsaken and Pure	Can deploy all levels of troops (no longer connected to faction research), slightly reduced deployment of Pure and Forsaken units
	
**Arthons	**

Arthon Scourge	Bloodlust
Arthon Tyrant	Speed 12 -> 18
Arthon Shieldbearer 	Gains Close Quarters Evade (25% Damage Resistance within 10 tiles, prevents limbs from being disabled with regular damage within range of 10 tiles), +4 Speed
Arthon Myrmidon	Paranoid (+4 perception +10 hearing range)
Arthon GL 	ER 11 -> 20
Arthon Acid GL	Acid 10 -> 20, ER 11 -> 20
Arthon Shieldbearer Prime	Skirmisher (+25% damage if damaged)
Arthon Evolved Acid GL	Acid 20 -> 30
Arthon Dragon Prime	Dash
Arthon Myrmidon Prime	Nightvision
Arthon Tyrant Prime	Master Marksman
	
**Tritons**

Triton Paralysing Arms	Paralysis value Increased 4 -> 8
Triton Evolved Paralysing Arm	Paralysis value 6 Increased -> 16
Triton Hitman and Marksamen	Gain Extreme Focus
Triton Thug Alpha	Replaced Harrower with Redemptor (KE), removed research requirements (Shredding Tech), Increased deployment cost by ~30%
Triton Marksman Alpha	Replaced Raven SR with Subjector (KE) , removed research requirements (Piercing Tech), Increased deployment cost by ~30%
Triton Maniac Alpha	Replaced Pirranha AR with Obliterator (KE) , removed research requirements (Piercing Tech), Increased deployment cost by ~30%
Trtion Ghoul and Ghoul Alpha	Improve Bloodsucking Arms to Evolved Bloodsucking Arms, adjusted AI 
	
**Chirons**

Worm Abdomen Chirons	"Ammo Increased 15 ->18 
Increased Shock Value when operative is hit by worm launcher 120 -> 240"
Blast Abdomen Chirons	Ammo Increased 12 -> 18
Acid Abdomen Chirons	Ammo Increased 12 -> 18, Acid Damage Value Increased 10 -> 20
Chiron Smaragdus Blast Abdomen	Ammo Increased 12 -> 30
Chiron Fuji, Nergal, Phthisis, Phasis (Melee Legs)	Speed Increased 12 -> 20
Chiron Phasis (Evolved Goo)	Living Crystal Aura (Applies Dazed dazed status to targets of ally melee attacks)
Chiron Nergal (Evolved Poisonworm)	Co-Poison (all enemies deal additional 10 poison with their attacks)
Chiron Fuji (Evolved Fireworm)	Fire Ward (all enemies gain Fire Resistance), Unlocks instantly if the player research Incendiary weapons technology (NJ) or Reverse Engineer Fire Grenade
Chiron Phthsis (Evolved Acidworm)	Armour Bane (all enemies deal additional 3 shred)
Chiron Smaragdus	Unlocks instantly if the player has researched Advanced Laser Weapons
	
**Sirens**

Siren Psychic Scream Ability	Psychic Scream AP reduced to 1 and can only be used once per turn
Siren Acid Spray	Adjusted cost and priority so it is used more often
Siren Mind Control Ability	Mind Control ability on the same turn as toggleable option in config (default: off), for people who enjoy suffering
All Sirens	Tail Armour 20 -> 30 (except Armis), All Siren Perception increased 30 -> 38
Siren Harbringer	Gains Close Quarters Evade (25% Damage Resistance within 10 tiles, prevents limbs from being disabled with regular damage within range of 10 tiles)
Siren Banshee	Replaced Buffing Head (Frenzy) with Screaming Head (Psychic Scream), WP 36 -> 42, Stealth 0% -> 50%, Speed 21 -> 25
Siren Armis	Gains Ignore Pain. Unlocks instantly if the player has researched Advanced Laser Weapons
Siren Injector Arms	Viral Value Increased 6 -> 10
	
**Skyllas**

Skylla	All abdomen armor value increased to 60, Gains Acid Resistance, WP increased by +150, Can now trample over small enemies (worms, spider drones etc) various AI adjustments to match
Skylla with Blaster Weapon	Attack is changed to instant, damage 80 - > 40, Shred 30 -> 3, can only be used once per turn, various tweaks to AI to make this behaviour possible
Skylla Smasher	Gained 8 Paralysis damage
Skylla Smaragdus	Gained Mind Control
	
**Other Enemies**

Mindfragger	Gains Acid explosion (acid value = 10) with 1 tile radius
Worms	All worms speed increased to 9 (from 6), worm explosion gets Shred 3, Food and Mutagen reward when processing captured worms decreased significantly
Terror Sentinel	Surveillance radius Increased 12 -> 18
Hatching Sentinel	Surveillance radius Increased 12 -> 18
All Eggs	Surveillance radius Increased 4 -> 7
Lair	Gains Co-corruption, 
Corruption Node	Gains Co-Corruption
	
**Human Perception Changed (Default off)**

All Humans Perception 	reduced to 30 (from 35). This can be adjusted in Config
Acheron Helmet	Pecception +7 -> +4
Acolyte Helmet	Pecception +4 -> +2
Aksu Helmet	Perception +10 -> +5, Willpower +0 -> +2
Amphion Body Armor	Perception +7 -> +4
Amphion Leg Armor	Pecception +4 -> +2
Anvil-2 Helmet	Perception -7 -> -2
Banshee Helmet	Pecception +5 -> +3
Echo Head	Perception +5 -> +3
Eidolon Helmet 	Perception +7 -> +4
Golem-B Helmet	Perception -5 -> 0
Guardian XA Helmet	Perception -7 -> -2
Perceptor Head	Pecerption +14 -> +8
Styx Helmet	Perception +10 -> +5
Tentacle Torso	Peception +6 -> +3
Disruptor Head	Perception +5 -> +3
