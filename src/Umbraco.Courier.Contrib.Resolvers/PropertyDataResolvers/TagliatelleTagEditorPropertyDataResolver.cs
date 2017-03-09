using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Courier.Core;
using Umbraco.Courier.Core.Attributes;
using Umbraco.Courier.Core.Enums;
using Umbraco.Courier.Core.Helpers;
using Umbraco.Courier.Core.Logging;
using Umbraco.Courier.DataResolvers;
using Umbraco.Courier.ItemProviders;

namespace Umbraco.Courier.Contrib.Resolvers.PropertyDataResolvers
{
    public class TagliatelleTagEditorPropertyDataResolver : PropertyDataResolverProvider
    {
        private const string ParentContainerPreValuePropertyAlias = "parentContainer";
        public override string EditorAlias
        {
            get
            {
                return "tagliatelle.tagEditor";
            }
        }

        public override void PackagingDataType(DataType item)
        {
            var parentContainer = item.Prevalues.SingleOrDefault(pv => pv.Alias == ParentContainerPreValuePropertyAlias);
            if (parentContainer == null || string.IsNullOrEmpty(parentContainer.Value))
            {
                return;
            }

            var parentContainerId = Dependencies.ConvertNodeIdToItemId(parentContainer.Value, ExecutionContext);
            if (parentContainerId == null)
            {
                return;
            }
            item.Dependencies.Add(parentContainerId);

            parentContainer.Value = parentContainerId.Id;
        }
    }
}