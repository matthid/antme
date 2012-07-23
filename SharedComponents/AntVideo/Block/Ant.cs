using AntMe.SharedComponents.States;

namespace AntMe.SharedComponents.AntVideo.Block {
    internal sealed class Ant : AntState, IUpdateable<AntUpdate, AntState>, ISerializable {
        #region Updateinformation

        private bool aIsAlive;
        private int aLoad;
        private LoadType aLoadType = LoadType.None;
        private int aTargetPositionX;
        private int aTargetPositionY;
        private TargetType aTargetType = TargetType.None;
        private int aVitality;
        private int dDirection;
        private int dPositionX;
        private int dPositionY;

        #endregion

        public Ant(Serializer serializer) : base(0, 0) {
            Deserialize(serializer);

            Reset();
        }

        public Ant(AntState zustand) : base(zustand.ColonyId, zustand.Id) {
            CasteId = zustand.CasteId;
            TargetType = zustand.TargetType;
            PositionX = zustand.PositionX;
            PositionY = zustand.PositionY;
            Direction = zustand.Direction;
            Vitality = zustand.Vitality;
            TargetPositionX = zustand.TargetPositionX;
            TargetPositionY = zustand.TargetPositionY;
            Load = zustand.Load;
            LoadType = zustand.LoadType;

            Reset();
        }

        private void Reset() {
            aTargetType = TargetType;
            aVitality = Vitality;
            dPositionX = 0;
            dPositionY = 0;
            dDirection = 0;
            aTargetPositionX = TargetPositionX;
            aTargetPositionY = TargetPositionY;
            aLoad = Load;
            aLoadType = LoadType;
        }

        #region IUpdateable<AntUpdate,AntState> Member

        public void Interpolate() {
            TargetType = aTargetType;
            Vitality = aVitality;
            PositionX = PositionX + dPositionX;
            PositionY = PositionY + dPositionY;
            Direction = Angle.Interpolate(Direction, dDirection);
            TargetPositionX = aTargetPositionX;
            TargetPositionY = aTargetPositionY;
            Load = aLoad;
            LoadType = aLoadType;
        }

        public void Update(AntUpdate update) {
            if (update.HasChanged(AntFields.TargetType)) {
                aTargetType = update.aTargetType;
            }
            if (update.HasChanged(AntFields.Vitality)) {
                aVitality = update.aVitality;
            }
            if (update.HasChanged(AntFields.Load)) {
                aLoad = update.aLoad;
            }
            if (update.HasChanged(AntFields.PositionX)) {
                dPositionX = update.dPositionX;
            }
            if (update.HasChanged(AntFields.PositionY)) {
                dPositionY = update.dPositionY;
            }
            if (update.HasChanged(AntFields.Direction)) {
                dDirection = update.dDirection;
            }
            if (update.HasChanged(AntFields.TargetPositionX)) {
                aTargetPositionX = update.aTargetPositionX;
            }
            if (update.HasChanged(AntFields.TargetPositionY)) {
                aTargetPositionY = update.aTargetPositionY;
            }
            if (update.HasChanged(AntFields.LoadType)) {
                aLoadType = update.aLoadType;
            }
        }

        public AntUpdate GenerateUpdate(AntState state) {
            AntUpdate update = new AntUpdate();
            update.Id = Id;
            bool changed = false;

            if (state.TargetType != TargetType) {
                update.Change(AntFields.TargetType);
                update.aTargetType = state.TargetType;
                changed = true;
            }

            if (state.PositionX != (PositionX + dPositionX)) {
                update.Change(AntFields.PositionX);
                update.dPositionX = state.PositionX - PositionX;
                changed = true;
            }

            if (state.PositionY != (PositionY + dPositionY)) {
                update.Change(AntFields.PositionY);
                update.dPositionY = state.PositionY - PositionY;
                changed = true;
            }

            if (state.Direction != Angle.Interpolate(Direction, dDirection)) {
                update.Change(AntFields.Direction);
                update.dDirection = Angle.Delta(Direction, state.Direction);
                changed = true;
            }

            if (state.TargetPositionX != TargetPositionX) {
                update.Change(AntFields.TargetPositionX);
                update.aTargetPositionX = state.TargetPositionX;
                changed = true;
            }

            if (state.TargetPositionY != TargetPositionY) {
                update.Change(AntFields.TargetPositionY);
                update.aTargetPositionY = state.TargetPositionY;
                changed = true;
            }

            if (state.Load != Load) {
                update.Change(AntFields.Load);
                update.aLoad = state.Load;
                changed = true;
            }

            if (state.Vitality != Vitality) {
                update.Change(AntFields.Vitality);
                update.aVitality = state.Vitality;
                changed = true;
            }

            if (state.LoadType != LoadType) {
                update.Change(AntFields.LoadType);
                update.aLoadType = state.LoadType;
                changed = true;
            }

            if (changed) {
                Update(update);
                return update;
            }
            return null;
        }

        public AntState GenerateState() {
            AntState state = new AntState(ColonyId, Id);
            state.TargetType = TargetType;
            state.Vitality = Vitality;
            state.Load = Load;
            state.PositionX = PositionX;
            state.PositionY = PositionY;
            state.Direction = Direction;
            state.CasteId = CasteId;
            state.TargetPositionX = TargetPositionX;
            state.TargetPositionY = TargetPositionY;
            state.LoadType = LoadType;
            return state;
        }

        public bool IsAlive {
            get { return aIsAlive; }
            set { aIsAlive = value; }
        }

        #endregion

        #region ISerializable Member

        // Blocklayout:
        // - ushort ID
        // - ushort ColonyID
        // - byte CasteId
        // - byte TargetType
        // - ushort PosX
        // - ushort PosY
        // - ushort Direction
        // - ushort Vitality
        // - ushort TargetPositionX
        // - ushort TargetPositionY
        // - byte Load
        // - byte LoadType

        public void Serialize(Serializer serializer) {
            serializer.SendUshort((ushort) Id);
            serializer.SendUshort((ushort) ColonyId);
            serializer.SendByte((byte) CasteId);
            serializer.SendByte((byte) TargetType);
            serializer.SendUshort((ushort) PositionX);
            serializer.SendUshort((ushort) PositionY);
            serializer.SendUshort((ushort) Direction);
            serializer.SendUshort((ushort) Vitality);
            serializer.SendUshort((ushort) TargetPositionX);
            serializer.SendUshort((ushort) TargetPositionY);
            serializer.SendByte((byte) Load);
            serializer.SendByte((byte) LoadType);
        }

        public void Deserialize(Serializer serializer) {
            Id = serializer.ReadUShort();
            ColonyId = serializer.ReadUShort();
            CasteId = serializer.ReadByte();
            TargetType = (TargetType) serializer.ReadByte();
            PositionX = serializer.ReadUShort();
            PositionY = serializer.ReadUShort();
            Direction = serializer.ReadUShort();
            Vitality = serializer.ReadUShort();
            TargetPositionX = serializer.ReadUShort();
            TargetPositionY = serializer.ReadUShort();
            Load = serializer.ReadByte();
            LoadType = (LoadType) serializer.ReadByte();
        }

        #endregion
    }
}