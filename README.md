# Better-Enemies
Various Enemy and AI adjustments aimed at making tactical combat more challenging and tactical, made by Phoenix Rising Mod Team 

# Features
- Various Improvements of Enemy AI and abilities to make enemies act "smarter"
- Various Adjustments of Enemy perks and stats to make many of them more difficult to fight against
- Adjusted base Perception value of all humans as well as equipment modifying perception, to make detection feel slightly more realistic (no more 60 tiles spotting through tiny hole in the wall)

Full Changelog here: https://docs.google.com/spreadsheets/d/1gCB-EpteJEuEnSTggOaF49PVPYjxTwc0cuZHbBUIIy4/

**AI Changes**

1. **Humans Obsessed with healing themself:**...................................................._Adjusted AI so they will only heal when have nothing else to do_
2. **Tritons with Bloodsucking arms use them over shooting weapons:**............................_Adjusted AI so they will prioritise shooting instead_
3. **Chirons with Stomper or Melee Legs leaving map when they run out of worms:**................_Adjusted AI to make this behaviour less likely (need testing)_
4. **Enemies with Dash and Melee Weapon (Better Classes Berserker Enemy, Vanilla: The Pure Assault with Vengeance Torso):**......._Adjusted AI to use Dash and follow it up with Melee Attack when possible_
5. **Skylla recieving major amount of Viral damage will always use recover:**....................._Adjusted AI to prevent this behaviour_
6. **Skylla:**.........................................._Skylla AI is adjusted to work with changed Blaster Cannon and Caterpillar Tracks (stomping effect)_
7. **Enemies with Quick Aim not using this skill:**....._Adjusted AI to use Quick Aim frequently (doesnt seem to be working at the moment)_
8. **Aspida:**.........................................._Fixed a bug with AI where it could not use Apollo LT and Themis NT weapons (if Kaos Engines was activated)_
9. **Armadillo:**......................................._Fixed a bug with AI where it could not use Mephistopheles FT weapon (if Kaos Engines was activated)_
10. **AI time to think:**..............................._Given an option in config (default: off) to double the time AI can think, increasing turn length but slightly improving decision making_
	
**General Changes**

1. **Pandoran Evolution:**.............._Add Arthon Gunner, Triton Wretch and Triton Footman to starting enemies. Removed Triton Strangler. (warning, this may lead to slightly faster than usual enemy evolutions)_
2. **Human Enemies:**..................._Human Enemies will deploy minimum of lvl 3 Troops, this also affect Haven Recruits and Defenders. Deployment points for lvl3 enemies decreased._
3. **Forsaken and Pure:**..............._Can deploy all levels of troops (no longer connected to faction research), slightly reduced deployment of Pure and Forsaken units_
	
**Arthons**

1. **Arthon Scourge**..................._Bloodlust_
2. **Arthon Tyrant**...................._Speed 12 -> 18_
3. **Arthon Shieldbearer**.............._Gains Close Quarters Evade (25% Damage Resistance within 10 tiles, prevents limbs from being disabled with regular damage within range of 10 tiles), +4 Speed_
4. **Arthon Myrmidon**.................._Paranoid (+4 perception +10 hearing range)_
5. **Arthon GL**........................_ER 11 -> 20_
6. **Arthon Acid GL**..................._Acid 10 -> 20, ER 11 -> 20_
7. **Arthon Shieldbearer Prime**........_Skirmisher (+25% damage if damaged)_
8. **Arthon Evolved Acid GL**..........._Acid 20 -> 30_
9. **Arthon Dragon Prime**.............._Dash_
10. **Arthon Myrmidon Prime**..........._Nightvision_
11. **Arthon Tyrant Prime**............._Master Marksman_
	
**Tritons**

1. **Triton Paralysing Arms**..........._Paralysis value Increased 4 -> 8_
2. **Triton Evolved Paralysing Arm**...._Paralysis value 6 Increased -> 16_
3. **Triton Hitman and Marksamen**......_Gain Extreme Focus_
4. **Triton Thug Alpha**................_Replaced Harrower with Redemptor (KE), removed research requirements (Shredding Tech), Increased deployment cost by ~30%_
5. **Triton Marksman Alpha**............_Replaced Raven SR with Subjector (KE) , removed research requirements (Piercing Tech), Increased deployment cost by ~30%_
6. **Triton Maniac Alpha**.............._Replaced Pirranha AR with Obliterator (KE) , removed research requirements (Piercing Tech), Increased deployment cost by ~30%_
7. **Trtion Ghoul and Ghoul Alpha**....._Improve Bloodsucking Arms to Evolved Bloodsucking Arms, adjusted AI_ 
	
**Chirons**

1. **Worm Abdomen Chirons**....................................._Ammo Increased 15 ->18  Increased Shock Value when operative is hit by worm launcher 120 -> 240
2. **Blast Abdomen Chirons**...................................._Ammo Increased 12 -> 18
3. **Acid Abdomen Chirons**....................................._Ammo Increased 12 -> 18, Acid Damage Value Increased 10 -> 20
4. **Chiron Smaragdus Blast Abdomen**..........................._Ammo Increased 12 -> 30
5. **Chiron Fuji, Nergal, Phthisis, Phasis (Melee Legs)**......._Speed Increased 12 -> 20
6. **Chiron Phasis (Evolved Goo)**.............................._Living Crystal Aura (Applies Dazed dazed status to targets of ally melee attacks)
7. **Chiron Nergal (Evolved Poisonworm)**......................._Co-Poison (all enemies deal additional 10 poison with their attacks)
8. **Chiron Fuji (Evolved Fireworm)**..........................._Fire Ward (all enemies gain Fire Resistance), Unlocks instantly if the player research Incendiary weapons technology (NJ) or Reverse Engineer Fire Grenade_
9. **Chiron Phthsis (Evolved Acidworm)**........................_Armour Bane (all enemies deal additional 3 shred)_
10. **Chiron Smaragdus**........................................_Unlocks instantly if the player has researched Advanced Laser Weapons_
	
**Sirens**

1. **Siren Psychic Scream Ability**....._Psychic Scream AP reduced to 1 and can only be used once per turn_
2. **Siren Acid Spray**................._Adjusted cost and priority so it is used more often_
3. **Siren Mind Control Ability**......._Mind Control ability on the same turn as toggleable option in config (default: off), for people who enjoy suffering_
4. **All Sirens**......................._Tail Armour 20 -> 30 (except Armis), All Siren Perception increased 30 -> 38_
5. **Siren Harbringer**................._Gains Close Quarters Evade (25% Damage Resistance within 10 tiles, prevents limbs from being disabled with regular damage within range of 10 tiles)_
6. **Siren Banshee**...................._Replaced Buffing Head (Frenzy) with Screaming Head (Psychic Scream), WP 36 -> 42, Stealth 0% -> 50%, Speed 21 -> 25_
7. **Siren Armis**......................_Gains Ignore Pain. Unlocks instantly if the player has researched Advanced Laser Weapons_
8. **Siren Injector Arms**.............._Viral Value Increased 6 -> 10_
	
**Skyllas**

1. **Skylla**..........................._All abdomen armor value increased to 60, Gains Acid Resistance, WP increased by +150, Can now trample over small enemies (worms, spider drones etc) various AI adjustments to match_
2. **Skylla with Blaster Weapon**......._Attack is changed to instant, damage 80 - > 40, Shred 30 -> 3, can only be used once per turn, various tweaks to AI to make this behaviour possible_
3. **Skylla Smasher**..................._Gained 8 Paralysis damage_
4. **Skylla Smaragdus**................._Gained Mind Control_
	
**Other Enemies**

1. **Mindfragger**......................_Gains Acid explosion (acid value = 10) with 1 tile radius_
2. **Worms**............................_All worms speed increased to 9 (from 6), worm explosion gets Shred 3, Food and Mutagen reward when processing captured worms decreased significantly_
3. **Terror Sentinel**.................._Surveillance radius Increased 12 -> 18_
4. **Hatching Sentinel**................_Surveillance radius Increased 12 -> 18_
5. **All Eggs**........................._Surveillance radius Increased 4 -> 7_
6. **Lair**............................._Gains Co-corruption,_
7. **Corruption Node**.................._Gains Co-Corruption_
	
**Human Perception Changed (Default off)**

1. **All Humans Perception**............_reduced to 30 (from 35). This can be adjusted in Config_
2. **Acheron Helmet**..................._Pecception +7 -> +4_
3. **Acolyte Helmet**..................._Pecception +4 -> +2_
4. **Aksu Helmet**......................_Perception +10 -> +5, Willpower +0 -> +2_
5. **Amphion Body Armor**..............._Perception +7 -> +4_
6. **Amphion Leg Armor**................_Pecception +4 -> +2_
7. **Anvil-2 Helmet**..................._Perception -7 -> -2_
8. **Banshee Helmet**..................._Pecception +5 -> +3_
9. **Echo Head**........................_Perception +5 -> +3_
10. **Eidolon Helmet**.................._Perception +7 -> +4_
11. **Golem-B Helmet**.................._Perception -5 -> 0_
12. **Guardian XA Helmet**.............._Perception -7 -> -2_
13. **Perceptor Head**.................._Pecerption +14 -> +8_
14. **Styx Helmet**....................._Perception +10 -> +5_
15. **Tentacle Torso**.................._Peception +6 -> +3_
16. **Disruptor Head**.................._Perception +5 -> +3_ 
