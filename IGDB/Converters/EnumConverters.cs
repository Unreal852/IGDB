using IGDBLib.Games;
using System;
using System.Collections.Generic;

namespace IGDBLib.Converters
{
    public static class EnumConverters
    {
        private static Dictionary<Type, Dictionary<int, Enum>> s_types = new Dictionary<Type, Dictionary<int, Enum>>()
        {
            {
                typeof(GameMode), new Dictionary<int, Enum>()
                {
                    { 1, GameMode.SINGLEPLAYER },
                    { 2, GameMode.MULTIPLAYER },
                    { 3, GameMode.CO_OPERATIVE },
                    { 4, GameMode.SPLIT_SCREEN },
                    { 5, GameMode.MASSIVELY_MULTIPLAYER }
                }
            },
            {
                typeof(GameStatus), new Dictionary<int, Enum>()
                {
                    { 0, GameStatus.RELEASED },
                    { 2, GameStatus.ALPHA },
                    { 3, GameStatus.BETA },
                    { 4, GameStatus.EARLY_ACCESS },
                    { 5, GameStatus.OFFLINE },
                    { 6, GameStatus.CANCELLED }
                }
            },
            {
                typeof(GameGenre), new Dictionary<int, Enum>()
                {
                    { 2, GameGenre.POINT_AND_CLICK },
                    { 4, GameGenre.FIGHTING },
                    { 5, GameGenre.SHOOTER },
                    { 7, GameGenre.MUSIC },
                    { 8, GameGenre.PLATFORM },
                    { 9, GameGenre.PUZZLE },
                    { 10, GameGenre.RACING },
                    { 11, GameGenre.REAL_TIME_STRATEGY },
                    { 12, GameGenre.ROLE_PLAYING },
                    { 13, GameGenre.SIMULATOR },
                    { 14, GameGenre.SPORT },
                    { 15, GameGenre.STRATEGY },
                    { 16, GameGenre.TURN_BASED_STRATEGY },
                    { 24, GameGenre.TACTICAL },
                    { 25, GameGenre.HACK_AND_SLASH_BEAT_EM_UP },
                    { 26, GameGenre.QUIZ },
                    { 30, GameGenre.PINBALL },
                    { 31, GameGenre.ADVENTURE },
                    { 32, GameGenre.INDIE },
                    { 33, GameGenre.ARCADE }
                }
            },
            {
                typeof(GameTheme), new Dictionary<int, Enum>()
                {
                    { 1, GameTheme.ACTION },
                    { 17, GameTheme.FANTASY },
                    { 18, GameTheme.SCIENCE_FICTION },
                    { 19, GameTheme.HORROR },
                    { 20, GameTheme.THRILLER },
                    { 21, GameTheme.SURVIVAL },
                    { 22, GameTheme.HISTORICAL },
                    { 23, GameTheme.STEALTH },
                    { 27, GameTheme.COMEDY },
                    { 28, GameTheme.BUSINESS },
                    { 31, GameTheme.DRAMA },
                    { 32, GameTheme.NON_FICTION },
                    { 33, GameTheme.SANDBOX },
                    { 34, GameTheme.EDUCATIONAL },
                    { 35, GameTheme.KIDS },
                    { 38, GameTheme.OPEN_WORLD },
                    { 39, GameTheme.WARFARE },
                    { 40, GameTheme.PARTY },
                    { 42, GameTheme.EROTIC },
                    { 43, GameTheme.MYSTERY }
                }
            },
            {
                typeof(GamePerspective), new Dictionary<int, Enum>()
                {
                    { 1, GamePerspective.FIRST_PERSON },
                    { 2, GamePerspective.THIRD_PERSON },
                    { 3, GamePerspective.BIRD_VIEW },
                    { 4, GamePerspective.SIDE_VIEW },
                    { 5, GamePerspective.TEXT },
                    { 6, GamePerspective.AURAL },
                    { 7, GamePerspective.VIRTUAL_REALITY }
                }
            }
        };

        /// <summary>
        /// Convert the given object into a GameEnum
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="targetType">Targeted Type</param>
        /// <returns>Converted Enum</returns>
        public static object Convert(object obj, Type targetEnumType)
        {
            int id;
            if (obj != null && int.TryParse(obj.ToString(), out id))
            {
                if (s_types.ContainsKey(targetEnumType) && s_types[targetEnumType].ContainsKey(id))
                    return s_types[targetEnumType][id];
            }
            return obj;
        }
    }
}
