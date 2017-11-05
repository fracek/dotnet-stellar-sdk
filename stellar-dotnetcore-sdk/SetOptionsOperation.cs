﻿using stellar_dotnetcore_sdk.xdr;
using System;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    ///<summary>
    /// Represents <a href="https://www.stellar.org/developers/learn/concepts/list-of-operations.html#set-options">SetOptions</a> operation.
    /// See <a href="https://www.stellar.org/developers/learn/concepts/list-of-operations.html">List of Operations</a>
    /// </summary>
    ///
    public class SetOptionsOperation : Operation
    {
        private readonly KeyPair _InflationDestination;
        private readonly int? _ClearFlags;
        private readonly int? _SetFlags;
        private readonly int? _MasterKeyWeight;
        private readonly int? _LowThreshold;
        private readonly int? _MediumThreshold;
        private readonly int? _HighThreshold;
        private readonly String _HomeDomain;
        private readonly SignerKey _Signer;
        private readonly int? _SignerWeight;

        public KeyPair InflationDestination => _InflationDestination;
        public int? ClearFlags => _ClearFlags;
        public int? SetFlags => _SetFlags;
        public int? MasterKeyWeight => _MasterKeyWeight;
        public int? LowThreshold => _LowThreshold;
        public int? MediumThreshold => _MediumThreshold;
        public int? HighThreshold => _HighThreshold;
        public string HomeDomain => _HomeDomain;
        public SignerKey Signer => _Signer;
        public int? SignerWeight => _SignerWeight;


        private SetOptionsOperation(KeyPair inflationDestination, int? clearFlags, int? setFlags,
                                    int? masterKeyWeight, int? lowThreshold, int? mediumThreshold,
                                    int? highThreshold, String homeDomain, SignerKey signer, int? signerWeight)
        {
            this._InflationDestination = inflationDestination;
            this._ClearFlags = clearFlags;
            this._SetFlags = setFlags;
            this._MasterKeyWeight = masterKeyWeight;
            this._LowThreshold = lowThreshold;
            this._MediumThreshold = mediumThreshold;
            this._HighThreshold = highThreshold;
            this._HomeDomain = homeDomain;
            this._Signer = signer;
            this._SignerWeight = signerWeight;
        }


        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            SetOptionsOp op = new SetOptionsOp();
            if (InflationDestination != null)
            {
                AccountID inflationDestination = new AccountID();
                inflationDestination.InnerValue = this.InflationDestination.XdrPublicKey;
                op.InflationDest = inflationDestination;
            }
            if (ClearFlags != null)
            {
                Uint32 clearFlags = new Uint32();
                clearFlags.InnerValue = this.ClearFlags.Value;
                op.ClearFlags = clearFlags;
            }
            if (SetFlags != null)
            {
                Uint32 setFlags = new Uint32();
                setFlags.InnerValue = SetFlags.Value;
                op.SetFlags = setFlags;
            }
            if (MasterKeyWeight != null)
            {
                Uint32 uint32 = new Uint32();
                uint32.InnerValue = MasterKeyWeight.Value;
                op.MasterWeight = uint32;
            }
            if (LowThreshold != null)
            {
                Uint32 uint32 = new Uint32();
                uint32.InnerValue = LowThreshold.Value;
                op.LowThreshold = uint32;
            }
            if (MediumThreshold != null)
            {
                Uint32 uint32 = new Uint32();
                uint32.InnerValue = MediumThreshold.Value;
                op.MedThreshold = uint32;
            }
            if (HighThreshold != null)
            {
                Uint32 uint32 = new Uint32();
                uint32.InnerValue = HighThreshold.Value;
                op.HighThreshold = uint32;
            }
            if (HomeDomain != null)
            {
                String32 homeDomain = new String32();
                homeDomain.InnerValue = this.HomeDomain;
                op.HomeDomain = homeDomain;
            }
            if (Signer != null)
            {
                sdkxdr.Signer signer = new sdkxdr.Signer();
                Uint32 weight = new Uint32();
                weight.InnerValue = SignerWeight.Value & 0xFF;
                signer.Key = this.Signer;
                signer.Weight = weight;
                op.Signer = signer;
            }

            sdkxdr.Operation.OperationBody body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(OperationType.OperationTypeEnum.SET_OPTIONS);
            body.SetOptionsOp = op;
            return body;
        }

        ///<summary>
        /// Builds SetOptions operation.
        ///</summary>
        ///<see cref="SetOptionsOperation"/>
        public class Builder
        {
            private KeyPair inflationDestination;
            private int? clearFlags;
            private int? setFlags;
            private int? masterKeyWeight;
            private int? lowThreshold;
            private int? mediumThreshold;
            private int? highThreshold;
            private String homeDomain;
            private SignerKey signer;
            private int? signerWeight;
            private KeyPair sourceAccount;

            public Builder(SetOptionsOp op)
            {
                if (op.InflationDest != null)
                {
                    inflationDestination = KeyPair.FromXdrPublicKey(
                            op.InflationDest.InnerValue);
                }
                if (op.ClearFlags != null)
                {
                    clearFlags = op.ClearFlags.InnerValue;
                }
                if (op.SetFlags != null)
                {
                    setFlags = op.SetFlags.InnerValue;
                }
                if (op.MasterWeight != null)
                {
                    masterKeyWeight = op.MasterWeight.InnerValue;
                }
                if (op.LowThreshold != null)
                {
                    lowThreshold = op.LowThreshold.InnerValue;
                }
                if (op.MedThreshold != null)
                {
                    mediumThreshold = op.MedThreshold.InnerValue;
                }
                if (op.HighThreshold != null)
                {
                    highThreshold = op.HighThreshold.InnerValue;
                }
                if (op.HomeDomain != null)
                {
                    homeDomain = op.HomeDomain.InnerValue;
                }
                if (op.Signer != null)
                {
                    signer = op.Signer.Key;
                    signerWeight = op.Signer.Weight.InnerValue & 0xFF;
                }
            }

            ///<summary>
            /// Creates a new SetOptionsOperation builder.
            ///</summary>
            public Builder() { }

            ///<summary>
            /// Sets the inflation destination for the account.
            ///</summary>
            ///<param name="inflationDestination">The inflation destination account.</param>
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetInflationDestination(KeyPair inflationDestination)
            {
                this.inflationDestination = inflationDestination;
                return this;
            }

            ///<summary>
            /// Clears the given flags from the account.
            ///</summary>
            ///<param name="clearFlags">For details about the flags, please refer to the <a href="https://www.stellar.org/developers/learn/concepts/accounts.html" target="_blank">accounts doc</a>.</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetClearFlags(int clearFlags)
            {
                this.clearFlags = clearFlags;
                return this;
            }

            ///<summary>
            /// Sets the given flags on the account.
            ///</summary>
            ///<param name="setFlags">For details about the flags, please refer to the <a href="https://www.stellar.org/developers/learn/concepts/accounts.html" target="_blank">accounts doc</a>.</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetSetFlags(int setFlags)
            {
                this.setFlags = setFlags;
                return this;
            }

            ///<summary>
            /// Weight of the master key.
            ///</summary>
            ///<param name="masterKeyWeight">Number between 0 and 255</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetMasterKeyWeight(int masterKeyWeight)
            {
                this.masterKeyWeight = masterKeyWeight;
                return this;
            }

            ///<summary>
            /// A number from 0-255 representing the threshold this account sets on all operations it performs that have a low threshold.
            ///</summary>
            ///<param name="lowThreshold">Number between 0 and 255</param>
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetLowThreshold(int lowThreshold)
            {
                this.lowThreshold = lowThreshold;
                return this;
            }

            ///<summary>
            /// A number from 0-255 representing the threshold this account sets on all operations it performs that have a medium threshold.
            ///</summary>
            ///<param name="mediumThreshold">Number between 0 and 255</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetMediumThreshold(int mediumThreshold)
            {
                this.mediumThreshold = mediumThreshold;
                return this;
            }

            ///<summary>
            /// A number from 0-255 representing the threshold this account sets on all operations it performs that have a high threshold.
            ///</summary>
            ///<param name="highThreshold">Number between 0 and 255</param>
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetHighThreshold(int highThreshold)
            {
                this.highThreshold = highThreshold;
                return this;
            }

            ///<summary>
            ///Sets the account's home domain address used in <a href="https://www.stellar.org/developers/learn/concepts/federation.html" target="_blank">Federation</a>.
            ///</summary>
            ///<param name="homeDomain">A string of the address which can be up to 32 characters.</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetHomeDomain(String homeDomain)
            {
                if (homeDomain.Length > 32)
                {
                    throw new ArgumentException("Home domain must be <= 32 characters");
                }
                this.homeDomain = homeDomain;
                return this;
            }

            ///<summary>
            /// Add, update, or remove a signer from the account. Signer is deleted if the weight = 0;
            ///</summary>
            ///<param name="signer">The signer key. Use <see cref="stellar_dotnetcore_sdk.Signer"/> helper to create this object.</param> 
            ///<param name="weight">The weight to attach to the signer (0-255).</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetSigner(SignerKey signer, int? weight)
            {
                this.signer = signer ?? throw new ArgumentNullException(nameof(signer), "signer cannot be null");

                if (weight == null)
                    throw new ArgumentNullException(nameof(weight), "weight cannot be null");

                signerWeight = weight.Value & 0xFF;
                return this;
            }

            ///<summary>
            /// Sets the source account for this operation.
            ///</summary>
            ///<param name="sourceAccount">The operation's source account.</param> 
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                this.sourceAccount = sourceAccount;
                return this;
            }

            ///<summary>
            ///Builds an operation
            ///</summary>
            public SetOptionsOperation Build()
            {
                SetOptionsOperation operation = new SetOptionsOperation(inflationDestination, clearFlags,
                        setFlags, masterKeyWeight, lowThreshold, mediumThreshold, highThreshold,
                        homeDomain, signer, signerWeight);
                if (sourceAccount != null)
                {
                    operation.SourceAccount = sourceAccount;
                }
                return operation;
            }
        }
    }
}