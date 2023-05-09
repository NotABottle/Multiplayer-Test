# Multiplayer Test

# Summary

This is a basic prototype of a multiplayer game, including steam integration, lobby, and game scene. Implemented using the FishNet and Mirror networking solutions and the Steamworks.Net wrapper for steam integration.

# Objective

The goal of this project was to learn the ins and outs of making a multiplayer game, how to add steam integration, and how working on a multiplayer project affects design choices and the general development cycle.

# Development

The development process for this project was an absolute nightmare to say the least. Going into this project I was, to an extent, aware of the difficulties of multiplayer development. However, I was not at all prepared for how much this project would change my entire understanding of design and implementation methods.

The greatest challenge of this project was understanding how data was synchronized between multiple clients, and how this synchronization had to occur and be planned for in clients that were using the same scripts.

For instance, to identify which player should control which object in the scene, an authority and connection ID system is used where on connection, an ID would be assigned to the client and then an object would be assigned to that ID. Any changes made on the client's end would be sent to the server, and the server would make the changes to the object with the designated ID, sending this new information back to the clients.

This is an example of a server authoritative infrastructure where the server verifies information from the clients and makes the changes accordingly. This choice of deciding which information is controlled by the client or by the server is one that obviously is not considered when making a single player game. However, in the multiplayer space it has a huge impact on the feel of the game.

If one is making a competitive multiplayer shooter, having a client authoritative can open the door to cheating, a server authoritative infrastructure would be better for this case. However, in a situation where cheating is a non-issue, a client authoritative infrastructure might be better suited.

# Conclusion

In conclusion, I learned a lot from this project about the difficulties of not only programming a multiplayer game but also designing one. I feel I completed my objective of this project and while development was obscenely difficult, I'm excited to try again and actually make a full on multiplayer title.
