export interface MeteoriteFilter {
    year?: YearFilter,
    recClass?: string,
    name?: string,
    sorting?: Sorting,
}

export interface YearFilter {
    startDateYear?: number,
    endDateYear?: number,
}

export interface Sorting {
    sortBy?: SortBy,
    sortOrder?: SortOrder,
}

export enum SortBy { Year, Count, SumMass, };
export enum SortOrder { Ascending, Descending, };
