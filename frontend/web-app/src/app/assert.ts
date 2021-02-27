const getErrorObject = () => {
    try { throw Error(''); } catch (err) { return err; }
};

export const assert = (assertion: boolean, msg: string | null = null) => {
    if (!assertion) {
        const caller = getErrorObject().stack.split('\n')[3];
        console.error(msg ? msg : `Assertion error - ${caller.trim()}`);
    }
};
