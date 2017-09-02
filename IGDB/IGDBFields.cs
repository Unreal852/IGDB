using System;

namespace IGDBLib
{
    /// <summary>
    /// IGDB Game Search field <see cref="https://igdb.github.io/api/endpoints/game/"/>
    /// </summary>
    public enum IGDBFields
    {
        /// <summary>
        /// Game ID <see cref="Int64"/>
        /// </summary>
        ID,
        /// <summary>
        /// Game Name <see cref="string"/>
        /// </summary>
        NAME,
        /// <summary>
        /// Game Slug <see cref="string"/>
        /// </summary>
        SLUG,
        /// <summary>
        /// Game URL <see cref="string"/>
        /// </summary>
        URL,
        /// <summary>
        /// Created At <see cref="Int64"/>
        /// </summary>
        CREATED_AT,
        /// <summary>
        /// Updated At <see cref="Int64"/>
        /// </summary>
        UPDATED_AT,
        /// <summary>
        /// Summary <see cref="string"/>
        /// </summary>
        SUMMARY,
        /// <summary>
        /// Storyline <see cref="string"/>
        /// </summary>
        STORYLINE,
        /// <summary>
        /// Collection <see cref="Int64[]"/>
        /// </summary>
        COLLECTION,
        /// <summary>
        /// Franchise <see cref="Int64[]"/>
        /// </summary>
        FRANCHISE,
        /// <summary>
        /// Hypes <see cref="int"/>
        /// </summary>
        HYPES,
        /// <summary>
        /// Popularity <see cref="double"/>
        /// </summary>
        POPULARITY,
        /// <summary>
        /// Rating <see cref="double"/>
        /// </summary>
        RATING,
        /// <summary>
        /// Rating count <see cref="int"/>
        /// </summary>
        RATING_COUNT,
        /// <summary>
        /// Aggregated rating <see cref="double"/>
        /// </summary>
        AGGREGATED_RATING,
        /// <summary>
        /// Aggregated rating count <see cref="int"/>
        /// </summary>
        AGGREGATED_RATING_COUNT,
        /// <summary>
        /// Total rating <see cref="double"/>
        /// </summary>
        TOTAL_RATING,
        /// <summary>
        /// Total rating count <see cref="int"/>
        /// </summary>
        TOTAL_RATING_COUNT,
        /// <summary>
        /// Weighted rating <see cref="double"/>
        /// </summary>
        WEIGHTED_RATING,
        /// <summary>
        /// Game <see cref="Int64"/>
        /// </summary>
        GAME,
        /// <summary>
        /// Developers <see cref="Int64[]"/>
        /// </summary>
        DEVELOPERS,
        /// <summary>
        /// Publishers <see cref="Int64[]"/>
        /// </summary>
        PUBLISHERS,
        /// <summary>
        /// Game Engines <see cref="Int64[]"/>
        /// </summary>
        GAME_ENGINES,
        /// <summary>
        /// Category <see cref="int"/>
        /// </summary>
        CATEGORY,
        /// <summary>
        /// Time to beat <see cref="object"/>
        /// </summary>
        TIME_TO_BEAT,
        /// <summary>
        /// Player perspectives <see cref="Int64[]"/>
        /// </summary>
        PLAYER_PERSPECTIVES,
        /// <summary>
        /// Game modes <see cref="Int64[]"/>
        /// </summary>
        GAME_MODES,
        /// <summary>
        /// Keywords <see cref="Int64[]"/>
        /// </summary>
        KEYWORDS,
        /// <summary>
        /// Themes <see cref="Int64[]"/>
        /// </summary>
        THEMES,
        /// <summary>
        /// Genres <see cref="Int64[]"/>
        /// </summary>
        GENRES,
        /// <summary>
        /// First release date <see cref="Int64"/>
        /// </summary>
        FIRST_RELEASE_DATE,
        /// <summary>
        /// Status <see cref="int"/>
        /// </summary>
        STATUS,
        /// <summary>
        /// Release Dates <see cref="object[]"/>
        /// </summary>
        RELEASE_DATES,
        /// <summary>
        /// Alternative names <see cref="object[]"/>
        /// </summary>
        ALTERNATIVE_NAMES,
        /// <summary>
        /// Screenshots <see cref="object[]"/>
        /// </summary>
        SCREENSHOTS,
        /// <summary>
        /// Videos <see cref="object[]"/>
        /// </summary>
        VIDEOS,
        /// <summary>
        /// Cover <see cref="object[]"/>
        /// </summary>
        COVER,
        /// <summary>
        /// ESRB rating <see cref="object"/>
        /// </summary>
        ESRB,
        /// <summary>
        /// PEGI rating <see cref="object"/>
        /// </summary>
        PEGI,
        /// <summary>
        /// Websites <see cref="object[]"/>
        /// </summary>
        WEBSITES,
        /// <summary>
        /// Standalone Expansions <see cref="Int64[]"/>
        /// </summary>
        STANDALONE_EXPANSIONS,
        /// <summary>
        /// Bundles <see cref="Int64[]"/>
        /// </summary>
        BUNDLES,
        /// <summary>
        /// Similar games <see cref="Int64[]"/>
        /// </summary>
        GAMES
    }
}
