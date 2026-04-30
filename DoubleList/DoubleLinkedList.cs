using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    public bool Contains(T data)
    {
        if (_head == null) return false;

        Node<T> actual = _head;
        while (actual != null)
        {
            // CompareTo retorna 0 si los valores son iguales
            if (actual.Data!.CompareTo(data) == 0)
            {
                return true;
            }
            actual = actual.Next!;
        }

        return false; // Se recorrió toda la lista y no se encontró
    }

    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
    }

    public void InsertOrdered(T data)
    {
        Node<T> newNode = new Node<T>(data);

        if (_head == null)
        {
            _head = _tail = newNode;
            return;
        }

        if (data.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
        else if (data.CompareTo(_tail!.Data) >= 0)
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
        else
        {
            Node<T>? currennt = _head;
            while (currennt != null && currennt.Data!.CompareTo(data) < 0)
            {
                currennt = currennt.Next;
            }

            newNode.Next = currennt;
            newNode.Previous = currennt!.Previous!;
            currennt.Previous!.Next = newNode;
            currennt.Previous = newNode;
        }
    }

    public void Remove(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                if (current == _head) // Found at the head
                {
                    _head = _head.Next;
                    _head!.Previous = null;
                }
                else if (current == _tail) // Found at the tail
                {
                    _tail = _tail.Previous;
                    _tail!.Next = null;
                }
                else // Found in the middle
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                }
                return;
            }
            current = current.Next;
        }
    }

    public void Reverse()
    {
        if (_head == null || _head.Next == null) return;

        Node<T> current = _head;
        Node<T> temp = null!;

        // Intercambiamos Siguiente y Anterior en cada nodo
        while (current != null)
        {
            temp = current.Previous!;
            current.Previous = current.Next;
            current.Next = temp;
            current = current.Previous; // Mover al que originalmente era el "Siguiente"
        }

        // El último nodo procesado (temp.Anterior) se convierte en la nueva Cabeza
        if (temp != null)
        {
            _tail = _head;
            _head = temp.Previous;
        }
    }

    public void Sort()
    {
        if (_head == null || _head.Next == null) return;

        bool exchanging;
        do
        {
            exchanging = false;
            Node<T> current = _head;

            while (current.Next != null)
            {
                // Si el actual es mayor que el siguiente, intercambiamos VALORES
                if (current.Data!.CompareTo(current.Next!.Data) > 0)
                {
                    T temp = current.Data!;
                    current.Data = current.Next!.Data;
                    current.Next!.Data = temp;
                    exchanging = true;
                }
                current = current.Next;
            }
        } while (exchanging);
    }

    override public string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }

    public string ToStringReverse()
    {
        var current = _tail;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Previous;
        }
        result += "null";
        return result;
    }
}