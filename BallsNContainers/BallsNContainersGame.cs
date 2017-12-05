using System;
using System.Collections.Generic;
using System.Linq;

namespace BallsNContainers
{
    public class BallsNContainersGame
    {
        private class Node
        {
            public Node LeftChildNode { get; set; }
            public Node RightChildNode { get; set; }
            public bool IsLeftChildNodeOpen { get; set; }
            public int MaxLevelNodeNum { get; set; }

            public Node()
            {
                MaxLevelNodeNum = -1;
            }
        }
        private readonly Node _rootNode = new Node();
        private readonly Random _rand = new Random();

        //Default constructor
        public BallsNContainersGame()
        {
            //Init 4-level tree
            InitGame(4);

            //Init default values
            _rootNode.IsLeftChildNodeOpen = true;
            _rootNode.LeftChildNode.IsLeftChildNodeOpen = false;
            _rootNode.LeftChildNode.LeftChildNode.IsLeftChildNodeOpen = true;
            _rootNode.LeftChildNode.LeftChildNode.LeftChildNode.IsLeftChildNodeOpen = false;
            _rootNode.LeftChildNode.LeftChildNode.RightChildNode.IsLeftChildNodeOpen = true;

            _rootNode.LeftChildNode.RightChildNode.IsLeftChildNodeOpen = true;
            _rootNode.LeftChildNode.RightChildNode.LeftChildNode.IsLeftChildNodeOpen = false;
            _rootNode.LeftChildNode.RightChildNode.RightChildNode.IsLeftChildNodeOpen = false;

            _rootNode.RightChildNode.IsLeftChildNodeOpen = false;
            _rootNode.RightChildNode.LeftChildNode.IsLeftChildNodeOpen = true;
            _rootNode.RightChildNode.LeftChildNode.LeftChildNode.IsLeftChildNodeOpen = true;
            _rootNode.RightChildNode.LeftChildNode.RightChildNode.IsLeftChildNodeOpen = true;

            _rootNode.RightChildNode.RightChildNode.IsLeftChildNodeOpen = true;
            _rootNode.RightChildNode.RightChildNode.LeftChildNode.IsLeftChildNodeOpen = false;
            _rootNode.RightChildNode.RightChildNode.RightChildNode.IsLeftChildNodeOpen = true;
        }

        //Constructor for creating the tree dynamically
        public BallsNContainersGame(uint maxNumOfLevels)
        {
            InitGame(maxNumOfLevels, true);
        }

        private void InitGame(uint maxNumOfLevels, bool randomState = false)
        {
            InitNode(_rootNode, 0, maxNumOfLevels - 1, randomState);
        }

        private int InitNode(Node node, int currentLevel, uint maxLevel, bool randomState = false, int maxLevelNodeNum = 0)
        {
            if (randomState)
            {
                node.IsLeftChildNodeOpen = _rand.NextDouble() < 0.5;
            }

            if (currentLevel >= maxLevel)
            {
                node.MaxLevelNodeNum = maxLevelNodeNum;
                return maxLevelNodeNum + 1;
            }

            int returnedMaxLevelNodeNum = maxLevelNodeNum;
            
            if (node.LeftChildNode == null)
            {
                node.LeftChildNode = new Node();
                returnedMaxLevelNodeNum = InitNode(node.LeftChildNode, currentLevel + 1, maxLevel, randomState, returnedMaxLevelNodeNum);
            }

            if (node.RightChildNode == null)
            {
                node.RightChildNode = new Node();
                returnedMaxLevelNodeNum = InitNode(node.RightChildNode, currentLevel + 1, maxLevel, randomState, returnedMaxLevelNodeNum);
            }

            return returnedMaxLevelNodeNum;
        }

        private Node BallPassesNode(Node node)
        {
            if (node.LeftChildNode == null && node.RightChildNode == null) return node;

            Node nextNode;
            if (node.IsLeftChildNodeOpen) nextNode = node.RightChildNode;
            else nextNode = node.LeftChildNode;
          
            return BallPassesNode(nextNode);
        }

        //Returns the index of the container which does not receive a ball.
        //Index starts from zero
        public int Play()
        {
            var foundNode = BallPassesNode(_rootNode);
            return foundNode.IsLeftChildNodeOpen ? foundNode.MaxLevelNodeNum * 2 + 1 : foundNode.MaxLevelNodeNum * 2;
        }
    }
}
