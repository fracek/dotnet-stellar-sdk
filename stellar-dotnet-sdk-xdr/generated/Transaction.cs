// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

using System;

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================
//  struct Transaction
//  {
//      // account used to run the transaction
//      AccountID sourceAccount;
//  
//      // the fee the sourceAccount will pay
//      uint32 fee;
//  
//      // sequence number to consume in the account
//      SequenceNumber seqNum;
//  
//      // validity range (inclusive) for the last ledger close time
//      TimeBounds* timeBounds;
//  
//      Memo memo;
//  
//      Operation operations<MAX_OPS_PER_TX>;
//  
//      // reserved for future use
//      union switch (int v)
//      {
//      case 0:
//          void;
//      }
//      ext;
//  };
//  ===========================================================================
    public class Transaction
    {
        public Transaction()
        {
        }

        public AccountID SourceAccount { get; set; }
        public Uint32 Fee { get; set; }
        public SequenceNumber SeqNum { get; set; }
        public TimeBounds TimeBounds { get; set; }
        public Memo Memo { get; set; }
        public Operation[] Operations { get; set; }
        public TransactionExt Ext { get; set; }

        public static void Encode(XdrDataOutputStream stream, Transaction encodedTransaction)
        {
            AccountID.Encode(stream, encodedTransaction.SourceAccount);
            Uint32.Encode(stream, encodedTransaction.Fee);
            SequenceNumber.Encode(stream, encodedTransaction.SeqNum);
            if (encodedTransaction.TimeBounds != null)
            {
                stream.WriteInt(1);
                TimeBounds.Encode(stream, encodedTransaction.TimeBounds);
            }
            else
            {
                stream.WriteInt(0);
            }

            Memo.Encode(stream, encodedTransaction.Memo);
            int operationssize = encodedTransaction.Operations.Length;
            stream.WriteInt(operationssize);
            for (int i = 0; i < operationssize; i++)
            {
                Operation.Encode(stream, encodedTransaction.Operations[i]);
            }

            TransactionExt.Encode(stream, encodedTransaction.Ext);
        }

        public static Transaction Decode(XdrDataInputStream stream)
        {
            Transaction decodedTransaction = new Transaction();
            decodedTransaction.SourceAccount = AccountID.Decode(stream);
            decodedTransaction.Fee = Uint32.Decode(stream);
            decodedTransaction.SeqNum = SequenceNumber.Decode(stream);
            int TimeBoundsPresent = stream.ReadInt();
            if (TimeBoundsPresent != 0)
            {
                decodedTransaction.TimeBounds = TimeBounds.Decode(stream);
            }

            decodedTransaction.Memo = Memo.Decode(stream);
            int operationssize = stream.ReadInt();
            decodedTransaction.Operations = new Operation[operationssize];
            for (int i = 0; i < operationssize; i++)
            {
                decodedTransaction.Operations[i] = Operation.Decode(stream);
            }

            decodedTransaction.Ext = TransactionExt.Decode(stream);
            return decodedTransaction;
        }

        public class TransactionExt
        {
            public TransactionExt()
            {
            }

            public int Discriminant { get; set; } = new int();

            public static void Encode(XdrDataOutputStream stream, TransactionExt encodedTransactionExt)
            {
                stream.WriteInt((int) encodedTransactionExt.Discriminant);
                switch (encodedTransactionExt.Discriminant)
                {
                    case 0:
                        break;
                }
            }

            public static TransactionExt Decode(XdrDataInputStream stream)
            {
                TransactionExt decodedTransactionExt = new TransactionExt();
                int discriminant = stream.ReadInt();
                decodedTransactionExt.Discriminant = discriminant;
                switch (decodedTransactionExt.Discriminant)
                {
                    case 0:
                        break;
                }

                return decodedTransactionExt;
            }
        }
    }
}