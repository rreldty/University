/// A model class used to represent a selectable item.
class MultiselectDto<V> {
  const MultiselectDto(this.value, this.label);

  final V value;
  final String label;
}
