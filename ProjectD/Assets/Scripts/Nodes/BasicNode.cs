using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An overall node that has a position in the room and defined connections to other nodes in the room.
 * 
 */
public class BasicNode : MonoBehaviour
{
    private List<BasicNode> connectingNodes = new List<BasicNode>();
    private List<double> connectionStrengths = new List<double>();

    private int nodeId;
    private List<int> friendList;

    
    void Start(){}

    void Update(){}


    public BasicNode(int nodeId, List<int> friendList)
    {
        this.nodeId = nodeId;
        this.friendList = friendList;
    }

    /**
     * Adds a new connection to this node. Connecting nodes can be accessed from getNextNode.
     * @param node The BasicNode to be connected.
     * @param strength The strength of the connection, meaning the relative probability (before normalisation) that this node is chosen from getNextNode.
     */
    public void addConnection(BasicNode node, double strength)
    {
        connectingNodes.Add(node);
        connectionStrengths.Add(strength);
    }


    /**
    * Sets this nodes connections. Connecting nodes can be accessed from getNextNode.
    * @param nodes The AINodes to be set as connections.
    * @param strengths The strength of the connections, meaning the relative probability (before normalisation) that these nodes is chosen from getNextNode.
    */
    public void setConnections(BasicNode[] nodes, double[] strengths)
    {
        connectingNodes.Clear();
        connectionStrengths.Clear();
        int i = 0;
        foreach (BasicNode node in nodes)
        {
            connectingNodes.Add(node);
            connectionStrengths.Add(strengths[i]);
            i++;
        }
    }


    /**
    * Returns a random node based on the current node's connection. The connecting nodes strengths is proportional to their chance of being chosen.
    * @return A random connecting node.
    */

    public BasicNode getNextNode()
    { //Returns a node based on connection strength.
        double strenghtSum = 0;
        foreach (double d in connectionStrengths)
        {
            strenghtSum += d;
        }
        double threshold = strenghtSum * Random.Range(0.0f, 1.0f);
        strenghtSum = 0;
        int i = 0;
        foreach (double d in connectionStrengths)
        {
            strenghtSum += d;
            if (strenghtSum > threshold)
            {
                break;
            }
            i++;
        }

        return connectingNodes[i];
    }

    public int getNodeId()
    {
        return nodeId;
    }

    /**
     *  Will add connections to other nodes and their corresponding strength.
     * @param nodeList , a list of nodes that this node should connect to.
     */
    public void init(List<BasicNode> nodeList)
    { //TODO use node strength. All connections have strength 1 atm.
        foreach (BasicNode node in nodeList)
        {
            if (friendList.Contains(node.getNodeId()))
            {
                addConnection(node, 1);
            }
        }
    }

    public List<int> getNodeFriendIds()
    {
        List<int> friends = new List<int>();
        foreach (int nodeFriend in friendList)
        {
            friends.Add(nodeFriend);
        }
        return friends;
    }

}

