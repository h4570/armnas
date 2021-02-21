export interface RAMInfo {
    /** Total physical amount of RAM */
    totalInKB: number;
    /** The amount of physical RAM, in kilobytes, left unused by the system. */
    freeInKB: number;
    /** An estimate of how much memory is available for starting new applications, without swapping */
    availableInKB: number;
}
