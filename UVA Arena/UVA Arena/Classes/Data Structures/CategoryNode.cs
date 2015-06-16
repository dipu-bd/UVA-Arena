using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace UVA_Arena.Structures
{
    /// <summary>
    /// File System like structure for categorize problems
    /// </summary>
    public class CategoryNode
    {
        public const char SEPARATOR = '\\';

        public CategoryNode(string name) { Title = name; }
        public CategoryNode() { Title = "Category " + GetHashCode().ToString(); }

        #region Fields

        /// <summary> Title of current category </summary>
        public string Title = "";
        /// <summary> Child category nodes </summary>
        public List<CategoryNode> Nodes = new List<CategoryNode>();
        /// <summary> List of all Problems that this category has </summary>
        public List<ProblemInfo> Problems = new List<ProblemInfo>();
        /// <summary> Parent category node if exist </summary>
        public CategoryNode Parent = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path to current category node
        /// </summary>
        public string Path
        {
            get
            {
                string res = Title;
                if (Parent != null)
                    res = Parent.Path + SEPARATOR + res;
                return res;
            }
        }

        /// <summary>
        /// Gets the level of current node int the tree
        /// </summary>
        public int Level
        {
            get
            {
                int res = 0;
                if (Parent != null)
                    res = Parent.Level + 1;
                return res;
            }
        }

        /// <summary>
        /// Give Access to index operator
        /// </summary>
        /// <param name="title">Title of child category to retrieve</param>
        /// <returns></returns>
        public CategoryNode this[string title]
        {
            get
            {
                return AddNode(title);
            }
            set
            {
                this.Nodes[IndexOfNode(title)] = value;
            }
        }


        public int Count
        {
            get { return Problems.Count; }
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(string))
                return Title.Equals(obj);
            else
                return base.Equals(obj);
        }

        public override string ToString()
        {
            return "Category: " + Title;
        }

        #endregion

        #region Add / Remove nodes

        /// <summary>
        /// Returns the first index of child category which name is equals of greater than the given value
        /// </summary>
        /// <param name="title">Title of the category to search for</param>
        /// <returns>First index which is greater of equals to the given category name</returns>
        /// <remarks>Copied from C++'s lower_bound method</remarks>
        public int IndexOfNode(string title)
        {
            int _first = 0;
            int _last = Nodes.Count;
            int _len = _last - _first;

            while (_len > 0)
            {
                int _half = _len >> 1;
                int _middle = _first + _half;

                if (Nodes[_middle].Title.CompareTo(title) < 0)
                {
                    _first = _middle + 1;
                    _len = _len - _half - 1;
                }
                else
                    _len = _half;
            }
            return _first;

        }

        /// <summary>
        /// Checks if the category title contains in the list
        /// </summary>
        /// <param name="title">Title of the category</param>
        /// <returns>True if category contains</returns>
        public bool Contains(string title)
        {
            int __i = IndexOfNode(title);
            return (__i < Nodes.Count && Nodes[__i].Title == title);
        }

        /// <summary>
        /// Adds a name to Nodes or returns the CategoryNode if already exists
        /// </summary>
        /// <param name="title">Title of new CategoryNode to add</param>
        /// <returns></returns>
        public CategoryNode AddNode(string path)
        {
            //get path parts 
            string _sub = path, _left = null;
            int _fin = path.IndexOf(SEPARATOR);
            if (_fin > 0)
            {
                _sub = path.Substring(0, _fin);
                _left = path.Substring(_fin + 1);
            }

            //get previous node
            CategoryNode _nod = null;
            int __i = IndexOfNode(_sub);
            if (__i < this.Nodes.Count && this.Nodes[__i].Title.CompareTo(_sub) == 0)
            {
                _nod = this.Nodes[__i];
            }

            //create and new node if necessary
            if (_nod == null)
            {
                _nod = new CategoryNode(_sub);
                _nod.Parent = this; 
                this.Nodes.Insert(__i, _nod); //__i as index is always rightly bounded
            }

            //add child node and return last child
            if (_left != null)
                return _nod.AddNode(_left);
            else
                return _nod;
        } 

        /// <summary>
        /// Removes a node by its name
        /// </summary>
        /// <param name="name">Title of the node to remove</param>
        public void RemoveNode(string title)
        {
            int i = IndexOfNode(title);
            if (i < Nodes.Count && Nodes[i].Title.CompareTo(title) == 0)
                Nodes.RemoveAt(i);
        }

        /// <summary>
        /// Deletes the current node from its parent node
        /// </summary>
        /// <exception cref="ArgumentNullException">If the parent node is null</exception>
        public void Delete()
        {
            this.Parent.RemoveNode(this.Title);
        }
        
        #endregion

        #region Add/Remove problems

        /// <summary>
        /// Index of problem by problem number. If none found length is returned
        /// </summary>
        /// <param name="pnum">Problem number to search for</param>
        /// <returns></returns>
        public int IndexOfProblem(long pnum)
        {
            int _first = 0;
            int _last = Problems.Count;
            int _len = _last - _first;

            while (_len > 0)
            {
                int _half = _len >> 1;
                int _middle = _first + _half;

                if (Problems[_middle].pnum < pnum)
                {
                    _first = _middle + 1;
                    _len = _len - _half - 1;
                }
                else
                    _len = _half;
            }
            return _first;
        }

        /// <summary>
        /// Determines if the given problem is in the problem list
        /// </summary>
        /// <param name="prob">ProblemInfo to search for</param>
        /// <returns>True if given ProblemInfo Contains in the list of Problems</returns>
        public bool HasProblem(ProblemInfo prob)
        {
            int index = IndexOfProblem(prob.pnum);
            return (index < Problems.Count && Problems[index] == prob);
        }

        /// <summary>
        /// Add a problem to current category
        /// </summary>
        /// <param name="problem"></param>
        /// <param name="savepath">True to add current path to problem's category</param>
        public void AddProblem(ProblemInfo problem, bool savepath = true)
        {
            //add to current and parents
            int index = IndexOfProblem(problem.pnum);
            if (index < Problems.Count && Problems[index] == problem) return;
            this.Problems.Insert(index, problem);
            if (this.Parent != null)
            {
                this.Parent.AddProblem(problem, false);
            }

            //add category to list
            if (savepath)
            {
                string _path = this.Path;
                if (!problem.categories.Contains(_path))
                    problem.categories.Add(_path);
            }
        }

        /// <summary>
        /// Removes a problem from current node and its parent nodes
        /// </summary>
        /// <param name="problem">Problem Info to remove</param>
        /// <returns>True if successfully removed from current node; otherwise false</returns>
        public bool RemoveProblem(ProblemInfo problem)
        {
            bool success = this.Problems.Remove(problem);
            if (success && Parent != null)
            {
                bool contains = false;
                foreach (CategoryNode cat in this.Parent.Nodes)
                {
                    if (cat.HasProblem(problem))
                    {
                        contains = true;
                        break;
                    }
                }
                //if none of parents child node contains this problem remove it
                if (!contains)
                {
                    this.Parent.RemoveProblem(problem);
                }
            }

            return success;
        }

        #endregion

    }
}
