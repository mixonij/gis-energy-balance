import {LocalKey} from "ts-localstorage";

export const colorModeKey = new LocalKey("layoutColorMode", "light", {
    hasDefaultValue: true,
});
export const themeKey = new LocalKey("theme", "bootstrap4-light-blue", {
    hasDefaultValue: true,
});
export const scaleKey = new LocalKey("scale", 12, {
    hasDefaultValue: true,
});

export const userEmailKey = new LocalKey("userEmail", "");
export const userPasswordHashKey = new LocalKey("userPasswordHash", "");