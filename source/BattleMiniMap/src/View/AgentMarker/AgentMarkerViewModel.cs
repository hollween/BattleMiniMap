﻿using System;
using BattleMiniMap.Config;
using BattleMiniMap.View.AgentMarker.Colors;
using BattleMiniMap.View.MapTerrain;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace BattleMiniMap.View.AgentMarker
{
    public class AgentMarkerViewModel : ViewModel
    {
        private Vec2 _position;
        private float _directionAsAngle;
        private Color _color;
        private AgentMarkerType _agentMarkerType;
        private Agent _agent;
        private float _alphaFactor;

        [DataSourceProperty]
        public float AlphaFactor
        {
            get => _alphaFactor;
            set
            {
                if (Math.Abs(_alphaFactor - value) < 0.01f)
                    return;
                _alphaFactor = value;
                OnPropertyChanged(nameof(AlphaFactor));
            }
        }

        [DataSourceProperty]
        public Vec2 Position
        {
            get => _position;
            set
            {
                if (_position == value)
                    return;
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        [DataSourceProperty]
        public float DirectionAsAngle
        {
            get => _directionAsAngle;
            set
            {
                _directionAsAngle = value;
                OnPropertyChanged(nameof(DirectionAsAngle));
            }
        }

        [DataSourceProperty]
        public Color Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        [DataSourceProperty]
        public AgentMarkerType AgentMarkerType
        {
            get => _agentMarkerType;
            set
            {
                if (_agentMarkerType == value)
                    return;
                _agentMarkerType = value;
                OnPropertyChanged(nameof(AgentMarkerType));
            }
        }

        public AgentMarkerViewModel(Agent agent)
        {
            _agent = agent;
            Update();
        }

        public void Update()
        {
            AlphaFactor = BattleMiniMapConfig.Get().Opacity;
            if (AgentMarkerType == AgentMarkerType.Dead)
                return;

            UpdateMarker();
        }

        private void UpdateMarker()
        {
            AgentMarkerType = _agent.GetAgentMarkerType();
            Color = AgentMarkerColorGenerator.GetAgentMarkerColor(_agent);
            if (AgentMarkerType == AgentMarkerType.Dead)
            {
                MakeDead();
                return;
            }

            var miniMap = MiniMap.Instance;
            if (!miniMap.IsEnabled && !BattleMiniMapConfig.Get().ShowMap)
                return;
            Position = miniMap.MapToWidget(miniMap.WorldToMapF(_agent.Position.AsVec2));
            //DirectionAsAngle = -_agent.GetMovementDirection().AsVec2.Normalized().AngleBetween(new Vec2(-1, 0));
        }

        private void MakeDead()
        {
            _agent = null;
        }
    }
}
