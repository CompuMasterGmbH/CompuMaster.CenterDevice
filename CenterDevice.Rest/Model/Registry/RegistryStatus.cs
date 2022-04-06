using System;

#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
namespace CenterDevice.Model.Registry
{
    [Flags]
    public enum RegistryStatus : uint
    {
        NONE = 0x0,
        RUNNING = 0x01,
        SYNC_CONFIRMED = 0x02,
        TOUCHED = 0x04,
        ERASE = 0x08,
        OWNED = 0x10,
        LOCKED = 0x20,
        LOCK_OWNED = 0x40,
        NEEDS_UPDATE = 0x80,
        ICON_OVERLAY_OUTDATED = 0x100,
        USER_NOT_AVAILABLE = 0x200,
        ARCHIVED = 0x400,
        NEEDS_INITIAL_SYNC = 0x800,
        SYNCED = 0x1000,
        SHARED = 0x2000,
        REMOTE = 0x4000,
        UNSYNC = 0x8000,

        ERROR_MAX_FOLDERS = 0x010000,
        ERROR_MAX_DEPTH = 0x020000,
        ERROR_INTERNAL = 0x040000,
        ERROR_NOT_FOUND = 0x100000,
        ERROR_FORBIDDEN = 0x200000
    }

    public static class RegistryStatusExtensions
    {
        public const uint ERROR_BITMASK = 0xFFFF0000;

        public const RegistryStatus FolderErrors = RegistryStatus.ERROR_MAX_DEPTH | RegistryStatus.ERROR_MAX_FOLDERS;

        public static bool HasError(this RegistryStatus status)
        {
            return ((uint)status & ERROR_BITMASK) != 0;
        }

        public static bool HasFolderError(this RegistryStatus runningState)
        {
            return ((int)runningState & (int)FolderErrors) > 0;
        }
    }
}
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element