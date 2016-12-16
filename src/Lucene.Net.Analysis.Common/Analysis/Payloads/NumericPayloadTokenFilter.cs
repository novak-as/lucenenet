﻿using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Util;
using System;

namespace Lucene.Net.Analysis.Payloads
{
    /*
	 * Licensed to the Apache Software Foundation (ASF) under one or more
	 * contributor license agreements.  See the NOTICE file distributed with
	 * this work for additional information regarding copyright ownership.
	 * The ASF licenses this file to You under the Apache License, Version 2.0
	 * (the "License"); you may not use this file except in compliance with
	 * the License.  You may obtain a copy of the License at
	 *
	 *     http://www.apache.org/licenses/LICENSE-2.0
	 *
	 * Unless required by applicable law or agreed to in writing, software
	 * distributed under the License is distributed on an "AS IS" BASIS,
	 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	 * See the License for the specific language governing permissions and
	 * limitations under the License.
	 */

    /// <summary>
    /// Assigns a payload to a token based on the <seealso cref="org.apache.lucene.analysis.Token#type()"/>
    /// 
    /// 
    /// </summary>
    public class NumericPayloadTokenFilter : TokenFilter
    {

        private string typeMatch;
        private BytesRef thePayload;

        private readonly IPayloadAttribute payloadAtt;
        private readonly ITypeAttribute typeAtt;

        public NumericPayloadTokenFilter(TokenStream input, float payload, string typeMatch) : base(input)
        {
            if (typeMatch == null)
            {
                throw new System.ArgumentException("typeMatch cannot be null");
            }
            //Need to encode the payload
            thePayload = new BytesRef(PayloadHelper.EncodeFloat(payload));
            this.typeMatch = typeMatch;
            this.payloadAtt = AddAttribute<IPayloadAttribute>();
            this.typeAtt = AddAttribute<ITypeAttribute>();
        }

        public override sealed bool IncrementToken()
        {
            if (input.IncrementToken())
            {
                if (typeAtt.Type.Equals(typeMatch))
                {
                    payloadAtt.Payload = thePayload;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}