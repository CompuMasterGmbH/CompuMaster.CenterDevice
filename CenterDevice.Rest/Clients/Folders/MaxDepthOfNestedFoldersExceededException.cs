﻿using CenterDevice.Model.Registry;
using System;

namespace CenterDevice.Rest.Clients.Folders
{
    [Serializable]
    public class MaxDepthOfNestedFoldersExceededException : Exceptions.PermanentException
    {
        public MaxDepthOfNestedFoldersExceededException() : base() { }

        public MaxDepthOfNestedFoldersExceededException(string message, Exception e) : base(message, e) { }

        public override RegistryStatus GetErrorCode()
        {
            return RegistryStatus.ERROR_MAX_DEPTH;
        }
    }
}
