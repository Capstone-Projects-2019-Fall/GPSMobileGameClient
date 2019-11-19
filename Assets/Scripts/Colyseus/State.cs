// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.5
// 

using Colyseus.Schema;

public class State : Schema {
	[Type(0, "map", typeof(MapSchema<ColyseusPlayer>))]
	public MapSchema<ColyseusPlayer> players = new MapSchema<ColyseusPlayer>();

	[Type(1, "number")]
	public float monsterHealth = 0;
}

